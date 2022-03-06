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
    public class UsersController : Controller
    {
        private LocationContext db = new LocationContext();

        // GET: Admin/Users
        public ActionResult Index()
        {
            if (!((bool)Session["is_admin"]))
            {
                return RedirectToAction("Forbidden", "../Admin");
            }
            return View(db.Clients.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_client,Nom_complet,Date_naissance,Telephone,Adresse_mail,Mots_de_passe,Image_cin,Image_permis,Etat_compte,isAdmin")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_client,Nom_complet,Date_naissance,Telephone,Adresse_mail,Mots_de_passe,Image_cin,Image_permis,Etat_compte,isAdmin")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateAccount()
        {
            int? id = int.Parse(Request["id_client"]);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            client.Etat_compte = "Validé";
            db.Entry(client).State = EntityState.Modified;

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
