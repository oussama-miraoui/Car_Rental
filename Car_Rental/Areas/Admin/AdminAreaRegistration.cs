using System.Web.Mvc;

namespace Car_Rental.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Car_Rental.Areas.Admin.Controllers" }

            );
        }
    }
}