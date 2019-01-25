using System.Web;
using System.Web.Mvc;

namespace jinalshah_coaching_class
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}