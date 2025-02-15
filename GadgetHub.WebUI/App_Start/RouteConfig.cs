using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "{category}/Page{page}",
                defaults: new { controller = "Product", action = "List", page = UrlParameter.Optional },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Search",
                url: "Product/Search",
                defaults: new { controller = "Product", action = "Search" }
            );
        }
    }
}
