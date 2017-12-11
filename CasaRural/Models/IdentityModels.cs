using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CasaRural.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Llogater> Llogaters { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var resultat = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

            if (entityEntry.Entity is Llogater &&
                (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified))
            {
                var llogater = entityEntry.Entity as Llogater;

                var telefonCorrecte = Regex.Match(llogater.Telefon, @"\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?").Success;
                var codiPostalCorrecte = Regex.Match(llogater.PostCode + "", @"^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$").Success;
                var NIFCorrecte = Regex.Match(llogater.NIF, @"^([0-9]{8}[A-Z])|[XYZ][0-9]{7}[A-Z]$").Success;
                var nomCognomsCorrecte = (llogater.NomCognoms.Length >= 20 && llogater.NomCognoms.Length <= 200);

                if (!telefonCorrecte)
                {
                    resultat.ValidationErrors.Add(
                        new System.Data.Entity.Validation.DbValidationError("Telefon",
                        "El format es incorecte!"));
                }

                if (!codiPostalCorrecte)
                {
                    resultat.ValidationErrors.Add(
                       new System.Data.Entity.Validation.DbValidationError("PostCode",
                        "El format es incorecte!"));
                }

                if (!NIFCorrecte)
                {
                    resultat.ValidationErrors.Add(
                       new System.Data.Entity.Validation.DbValidationError("NIF",
                        "El format es incorecte!"));
                }

                if (!nomCognomsCorrecte)
                {
                    resultat.ValidationErrors.Add(
                       new System.Data.Entity.Validation.DbValidationError("NomCognoms",
                        "El nom i cognoms introduïts no compleixen els requisits!"));
                }
            }

            if (entityEntry.Entity is Reserva &&
                (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified))
            {
                var reserva = entityEntry.Entity as Reserva;

                var DataEntrada = reserva.DataEntrada;
                var DataSortida = reserva.DataSortida;
                var DataActual = DateTime.Now;

                if (DataEntrada > DataSortida)
                {
                    resultat.ValidationErrors.Add(
                        new System.Data.Entity.Validation.DbValidationError("DataEntrada",
                        "La data d'entrada no pot ser més gran que la data de sortida!"));
                    resultat.ValidationErrors.Add(
                        new System.Data.Entity.Validation.DbValidationError("DataSortida",
                        "La data de sortida no pot ser més petita que la data d'Entrada!"));
                }
                if (DataEntrada.Date <= DataActual.Date)
                {
                    resultat.ValidationErrors.Add(
                       new System.Data.Entity.Validation.DbValidationError("DataEntrada",
                       "La data d'entrada ha de ser registrada com a minim 24 hores mes tard que la data actual!"));
                }
            }


            if (resultat.ValidationErrors.Any())
            {
                //hem afegit errors a la llista d'errors de validació, retornem la llista d'errors
                return resultat;
            }
            else
            {
                //no hi ha hagut errors de validació, fem les validacions automàtiques de la classe base
                return base.ValidateEntity(entityEntry, items);
            }

        }
    }
}