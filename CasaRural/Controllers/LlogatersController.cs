using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasaRural.Models;

namespace CasaRural.Controllers
{
    public class LlogatersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Llogaters
        public ActionResult Index()
        {
            return View(db.Llogaters.ToList());
        }

        // GET: Llogaters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Llogater llogater = db.Llogaters.Find(id);
            if (llogater == null)
            {
                return HttpNotFound();
            }
            return View(llogater);
        }

        // GET: Llogaters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Llogaters/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NIF,NomCognoms,Telefon,PostCode")] Llogater llogater)
        {
            if (ModelState.IsValid)
            {
                db.Llogaters.Add(llogater);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationError in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationError.ValidationErrors)
                        {
                            this.ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                    return View(llogater);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Llogaters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Llogater llogater = db.Llogaters.Find(id);
            if (llogater == null)
            {
                return HttpNotFound();
            }
            return View(llogater);
        }

        // POST: Llogaters/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NIF,NomCognoms,Telefon,PostCode")] Llogater llogater)
        {
            if (ModelState.IsValid)
            {
                db.Entry(llogater).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationError in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationError.ValidationErrors)
                        {
                            this.ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                    return View(llogater);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Llogaters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Llogater llogater = db.Llogaters.Find(id);
            if (llogater == null)
            {
                return HttpNotFound();
            }
            return View(llogater);
        }

        // POST: Llogaters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Llogater llogater = db.Llogaters.Find(id);
            db.Llogaters.Remove(llogater);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
