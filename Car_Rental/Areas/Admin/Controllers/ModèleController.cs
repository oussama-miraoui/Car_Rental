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
    public class ModèleController : Controller
    {
        private LocationContext db = new LocationContext();

        // GET: Admin/Modèle
        public ActionResult Index()
        {
            return View(db.Modèles.ToList());
        }

        // GET: Admin/Modèle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modèle modèle = db.Modèles.Find(id);
            if (modèle == null)
            {
                return HttpNotFound();
            }
            return View(modèle);
        }

        // GET: Admin/Modèle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Modèle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_modele,Marque,Serie")] Modèle modèle)
        {
            if (ModelState.IsValid)
            {
                db.Modèles.Add(modèle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modèle);
        }

        // GET: Admin/Modèle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modèle modèle = db.Modèles.Find(id);
            if (modèle == null)
            {
                return HttpNotFound();
            }
            return View(modèle);
        }

        // POST: Admin/Modèle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_modele,Marque,Serie")] Modèle modèle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modèle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modèle);
        }

        // GET: Admin/Modèle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modèle modèle = db.Modèles.Find(id);
            if (modèle == null)
            {
                return HttpNotFound();
            }
            db.Modèles.Remove(modèle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/Modèle/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Modèle modèle = db.Modèles.Find(id);
        //    db.Modèles.Remove(modèle);
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
