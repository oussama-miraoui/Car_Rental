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
    public class LocationsController : Controller
    {
        private LocationContext db = new LocationContext();

        // GET: Admin/Locations
        public ActionResult Index()
        {
            var locations = db.Locations.Include(l => l.Client).Include(l => l.Voiture);
            return View(locations.ToList());
        }

        // GET: Admin/Locations/Delete/5
        public ActionResult Delete(int? idC, int? idV)
        {
            if (idC == null || idV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(idC, idV);
            if (location == null)
            {
                return HttpNotFound();
            }
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateReservation()
        {

            int? idC = int.Parse(Request["id_client"]);
            int? idV = int.Parse(Request["id_voiture"]);


            if (idC == null || idV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var location = db.Locations.Find(idC, idV);
            if (location == null)
            {
                return HttpNotFound();
            }
            //
            location.Etat_location = "Validé";

            db.Entry(location).State = EntityState.Modified;
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

