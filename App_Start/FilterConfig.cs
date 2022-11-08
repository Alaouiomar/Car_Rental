using System.Web;
using System.Web.Mvc;

namespace LOCATION_DES_VOITURES
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
