using System.Web.Mvc;

namespace jinalshah_coaching_class.Areas.Admin
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
                new {area="Admin",controller="Admin_blog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
