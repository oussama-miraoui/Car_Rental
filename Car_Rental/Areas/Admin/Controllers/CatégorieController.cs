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
    public class CatégorieController : Controller
    {
        private LocationContext db = new LocationContext();

        // GET: Admin/Catégorie
        public ActionResult Index()
        {
            return View(db.Catégories.ToList());
        }

        // GET: Admin/Catégorie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catégorie catégorie = db.Catégories.Find(id);
            if (catégorie == null)
            {
                return HttpNotFound();
            }
            return View(catégorie);
        }

        // GET: Admin/Catégorie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Catégorie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_categorie,Nom")] Catégorie catégorie)
        {
            if (ModelState.IsValid)
            {
                db.Catégories.Add(catégorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catégorie);
        }

        // GET: Admin/Catégorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catégorie catégorie = db.Catégories.Find(id);
            if (catégorie == null)
            {
                return HttpNotFound();
            }
            return View(catégorie);
        }

        // POST: Admin/Catégorie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_categorie,Nom")] Catégorie catégorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catégorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catégorie);
        }

        // GET: Admin/Catégorie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catégorie catégorie = db.Catégories.Find(id);
            if (catégorie == null)
            {
                return HttpNotFound();
            }
            db.Catégories.Remove(catégorie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/Catégorie/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Catégorie catégorie = db.Catégories.Find(id);
        //    db.Catégories.Remove(catégorie);
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
