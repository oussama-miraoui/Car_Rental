using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Rental.Areas.Admin.Controllers
{
    public class ForbiddenController : Controller
    {
        // GET: Admin/Forbidden
        public ActionResult Index()
        {
            return View();
        }
    }
}