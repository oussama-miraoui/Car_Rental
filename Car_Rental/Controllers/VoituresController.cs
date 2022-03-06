using Car_Rental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Car_Rental.Controllers
{
    public class VoituresController : Controller
    {
        private LocationContext db = new LocationContext();

        // GET: Voitures
        public ActionResult Index(/*string carburant*/)
        {
            var voitures = db.Voitures.Include(v => v.Catégorie).Include(v => v.Modèle);

            //if (!string.IsNullOrWhiteSpace(carburant))
            //{
            //    voitures = voitures.Where(v => v.Type_carburant.ToLower().Contains(carburant.ToLower()));
            //}
            
            return View(voitures);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var voiture = db.Voitures.
                Include(v => v.Catégorie).
                Include(v => v.Modèle).
                SingleOrDefault(x => x.Id_voiture == id);


            if (voiture == null)
            {
                return HttpNotFound();
            }

            if(db.Locations.Where(loc=>loc.Id_voiture == id).Count() > 0)
            {
                TempData["voiture_loué"] = "Loué";
            }
            return View(voiture);
        }

        public ActionResult Location()
        {
            if (Session["id_client"] == null)
            {
                TempData["login_first"] = "Vous deviez se connecter pour réserver une voiture! :)";
                return RedirectToAction("Login", "Accounts");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location(Location location, int? id_voiture)
        {
            try
            {
                if (id_voiture == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //
                var voiture = db.Voitures.Find(id_voiture);
                int duree_location = (location.date_fin - location.date_debut).Days;
                double prix_total;

                //
                location.id_client = (int)Session["id_client"];
                location.Id_voiture = (int)id_voiture;
                location.Etat_location = "En attente";

                if (duree_location > 29)
                {
                    location.type_location = "Longue durée";
                    prix_total = voiture.Prix_location * duree_location * 0.6;
                }
                else
                {
                    location.type_location = "Courte durée";
                    prix_total = voiture.Prix_location * duree_location;
                }

                //
                location.prix_location_total = prix_total;


                db.Locations.Add(location);
                db.SaveChanges();

                TempData["attente_validation_agent"] = "Votre réservation est en attente d'un agent de la valider.";

                return RedirectToAction("ReservationList", "Voitures");
            }
            catch (Exception)
            {
                TempData["voiture_deja_reserve"] = "Vous avez déjà réservé cette voiture.";
                return View();
            }
            
        }
        public ActionResult ReservationList()
        {
            if (Session["id_client"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            int id_client = (int)Session["id_client"];

            var reservationList = db.Locations.
                                  Include(loc => loc.Voiture).
                                  Include(loc=>loc.Client).
                                  Where(loc => loc.id_client == id_client);

            if(reservationList == null)
            {
                return HttpNotFound();
            }

            return View(reservationList);
        }
        public ActionResult cancelReservation(int? idC, int? idV) 
        {   if(idC == null || idV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservation = db.Locations.Find(idC, idV);
            if(reservation == null)
            {
                return HttpNotFound();
            }
            db.Locations.Remove(reservation);
            db.SaveChanges();
            TempData["cancelReservation"] = "Réservation annulée!";
            return RedirectToAction("ReservationList");
        }
    }
}