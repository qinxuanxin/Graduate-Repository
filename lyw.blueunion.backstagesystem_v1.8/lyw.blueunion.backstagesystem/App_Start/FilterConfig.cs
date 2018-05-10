using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Filters;
namespace lyw.blueunion.backstagesystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CheckPermissionFilter());
        }
    }
}