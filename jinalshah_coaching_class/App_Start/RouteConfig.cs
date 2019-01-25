using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace jinalshah_coaching_class
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Visitor_home", action = "home_visitor", id =2 }
             
              
                );
               
                //defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }

            

          

        }
    }
}