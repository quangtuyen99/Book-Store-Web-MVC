using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //Categories
            routes.MapRoute("Product", "{type}/{meta}",
            new { controller = "Product", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "san-pham" }
            },
            namespaces: new[] { "BookStore.Controllers" });

            //Menu Shop
            routes.MapRoute("Shop", "shop",
            new { controller = "Shop", action = "Shop", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });
            //Menu About
            routes.MapRoute("About", "about",
            new { controller = "About", action = "About", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });
            //Menu Faq
            routes.MapRoute("Faq", "faq",
            new { controller = "Faq", action = "Faq", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });
            //Menu Login
            routes.MapRoute("Login", "login",
            new { controller = "Login", action = "Login", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });
            //Menu Home
            routes.MapRoute("Home", "home",
            new { controller = "Default", action = "Index", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });

            //Detail product
            routes.MapRoute("Detail", "{type}/{meta}/{id}",
            new { controller = "Product", action = "Detail", meta = UrlParameter.Optional },
             new RouteValueDictionary
            {
                { "type", "san-pham" }
            },
            namespaces: new[] { "BookStore.Controllers" });
            //Play audio

            routes.MapRoute("Collecttion", "collection",
            new { controller = "Collect", action = "Index", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });

            //Play
            routes.MapRoute("Play", "{type}/{meta}/{id}/play",
            new { controller = "Play", action = "Index", meta = UrlParameter.Optional },
             new RouteValueDictionary
            {
                { "type", "san-pham" }
            },
            namespaces: new[] { "BookStore.Controllers" });


            routes.MapRoute("Register", "register",
            new { controller = "User", action = "AddOrEdit", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });

            routes.MapRoute("Audio", "audio",
            new { controller = "Play", action = "UploadAudio", meta = UrlParameter.Optional },
            namespaces: new[] { "BookStore.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BookStore.Controllers" }
            );

            
        }
    }
}
