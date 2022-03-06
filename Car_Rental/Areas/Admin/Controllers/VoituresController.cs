using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car_Rental.Models;

namespace Car_Rental.Areas.Admin.Controllers
{
    public class VoituresController : Controller
    {
        private LocationContext db = new LocationContext();

        // GET: Admin/Voitures
        public ActionResult Index()
        {
            var voitures = db.Voitures.Include(v => v.Catégorie).Include(v => v.Modèle);
            if (!((bool)Session["is_admin"]))
            {
                return RedirectToAction("Forbidden", "../Admin");
            }
            return View(voitures.ToList());
        }

        // GET: Admin/Voitures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // GET: Admin/Voitures/Create
        public ActionResult Create()
        {
            ViewBag.Id_categorie = new SelectList(db.Catégories, "Id_categorie", "Nom");
            ViewBag.Id_modele = new SelectList(db.Modèles, "Id_modele", "Marque");
            return View();
        }

        // POST: Admin/Voitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_voiture,Image,Date_mise_en_circulation,Type_carburant,Prix_location,Id_categorie,Id_modele")] Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.Voitures.Add(voiture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_categorie = new SelectList(db.Catégories, "Id_categorie", "Nom", voiture.Id_categorie);
            ViewBag.Id_modele = new SelectList(db.Modèles, "Id_modele", "Marque", voiture.Id_modele);
            return View(voiture);
        }

        // GET: Admin/Voitures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_categorie = new SelectList(db.Catégories, "Id_categorie", "Nom", voiture.Id_categorie);
            ViewBag.Id_modele = new SelectList(db.Modèles, "Id_modele", "Marque", voiture.Id_modele);
            return View(voiture);
        }

        // POST: Admin/Voitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_voiture,Image,Date_mise_en_circulation,Type_carburant,Prix_location,Id_categorie,Id_modele")] Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voiture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_categorie = new SelectList(db.Catégories, "Id_categorie", "Nom", voiture.Id_categorie);
            ViewBag.Id_modele = new SelectList(db.Modèles, "Id_modele", "Marque", voiture.Id_modele);
            return View(voiture);
        }

        // GET: Admin/Voitures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            db.Voitures.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/Voitures/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Voiture voiture = db.Voitures.Find(id);
        //    db.Voitures.Remove(voiture);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
