using System.Web.Mvc;

namespace jinalshah_coaching_class.Areas.User_data
{
    public class User_dataAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User_data";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_data_default",
                "User_data/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
