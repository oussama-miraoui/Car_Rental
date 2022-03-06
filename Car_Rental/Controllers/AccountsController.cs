using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car_Rental.Models;

namespace Car_Rental.Controllers
{
    public class AccountsController : Controller
    {
        private LocationContext db = new LocationContext();

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Client client)
        {
            if (db.Clients.Any(c => c.Adresse_mail == client.Adresse_mail))
            {
                TempData["Email_existe"] = "Email existe déjà.";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    client.Etat_compte = "En attente";

                    if (client.Adresse_mail.Contains("admin"))
                    {
                        client.isAdmin = true;
                    }
                    else
                    {
                        client.isAdmin = false;
                    }

                    db.Clients.Add(client);
                    db.SaveChanges();
                    Session["id_client"] = client.Id_client;
                    Session["nom_client"] = client.Nom_complet;
                    Session["email_client"] = client.Adresse_mail;
                    Session["is_admin"] = client.isAdmin;

                    //if user is Admin redirect to admin page otherwise to index

                    //if ((bool)Session["is_admin"])
                    //{
                    //    return RedirectToAction("Index", "Admin/Dashboard");
                    //}

                    return RedirectToAction("Index", "Voitures");
                }
                return View();
            }

        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Client client)
        {
            var login = db.Clients.Where(c => c.Adresse_mail.Equals(client.Adresse_mail) && c.Mots_de_passe == client.Mots_de_passe).FirstOrDefault();

            if (login != null)
            {
                Session["id_client"] = login.Id_client;
                Session["nom_client"] = login.Nom_complet;
                Session["email_client"] = login.Adresse_mail;
                Session["is_admin"] = login.isAdmin;

                ////if user is Admin redirect to admin page otherwise to index

                //if ((bool)Session["is_admin"])
                //{
                //    return RedirectToAction("Index", "Admin");
                //}

                return RedirectToAction("Index", "Voitures");
            }
            else
            {
                TempData["login_error"] = "Informations incorrects";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Accounts");
        }
    }
}