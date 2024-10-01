using System.Web;
using System.Web.Mvc;

namespace _21520613_SE310P11_ASPNET_QLSP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
