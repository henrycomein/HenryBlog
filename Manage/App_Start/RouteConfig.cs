using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Manage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("webapi", "api/{controller}/{action}/{id}", new { controller = "Article", action = "List", id = UrlParameter.Optional });
            routes.MapRoute("login", "login", new { controller = "Home", action = "login" });
            routes.MapRoute("logout", "logout", new { controller = "Home", action = "logout" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
          
        }
    }
}