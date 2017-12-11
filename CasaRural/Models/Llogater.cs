using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CasaRural.Models
{
    public class Llogater
    {
        [Key]
        public string NIF { get; set; }
        [Display(Name = "Nom i Cognoms")]
        public string NomCognoms { get; set; }
        public string Telefon { get; set; }
        [Display(Name = "Codi postal")]
        public int PostCode { get; set; }

        public virtual List<Reserva> Reservas { get; set; }
    }
}