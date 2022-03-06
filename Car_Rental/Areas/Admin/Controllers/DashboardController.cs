using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car_Rental.Models;

namespace Car_Rental.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private LocationContext db = new LocationContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var tupleModel = new Tuple<List<Client>, List<Voiture>, List<Location>>(db.Clients.ToList(), db.Voitures.ToList(), db.Locations.ToList());

            if (Session["id_client"] == null)
            {
                TempData["login_first"] = "Vous deviez se connecter autant d'admin pour accéder a cette page! :)";
                return RedirectToAction("Login", "../Accounts");
            }
            if (!((bool)Session["is_admin"]))
            {
                return RedirectToAction("Forbidden", "../Admin");
            }

            return View(tupleModel);
        }
    }
}