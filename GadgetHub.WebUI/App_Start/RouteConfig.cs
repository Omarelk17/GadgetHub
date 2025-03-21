﻿using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new
                {
                    controller = "Product",
                    action = "List"
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Search",
                url: "Product/Search",
                defaults: new { controller = "Product", action = "Search" }
            );

            // Ensure this route for CartController's Checkout action
            routes.MapRoute(
                name: "Checkout",
                url: "Order/Checkout",
                defaults: new { controller = "Cart", action = "Checkout" }
            );
        }
    }
}
