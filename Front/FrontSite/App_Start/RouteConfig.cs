using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FrontSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute("AllArticleList","articlelist.html", new {controller="Home",action="OpenSource"});
            routes.MapRoute("OpenSource","opensourceproject.html",new {controller="Home",action="OpenSource"});
            routes.MapRoute("AboutMe","about.html",new { controller = "Home", action = "About" });
            
            routes.MapRoute("photolist","photo.html", new { controller = "Fun", action = "Photo" });
            routes.MapRoute("photoshow","photoview/{id}", new { controller = "Fun", action = "PhotoView" }, new { id=@"\d+"});
            routes.MapRoute("event","event.html",new { controller = "Fun", action = "LifeEvent" });

            routes.MapRoute("checkpassword", "valicate/{action}", new { controller = "Fun" });

            routes.MapRoute(
               name: "Blog",
               url: "blog/{basekey}.html",
               defaults: new { controller = "Blog", action = "Switch", basekey = "list" }
           );
            routes.MapRoute(
               name: "BlogArticleList",
               url: "blog/article/list/{id}.html",
               defaults: new { controller = "Blog", action = "ArticleList" },
               constraints: new { id = @"\d+" }
           );
            routes.MapRoute(
                name: "BlogArticleShow",
                url: "blog/article/view/{id}.html",
                defaults: new { controller = "Blog", action = "ArticleDetail" },
                constraints: new { id = @"\d+" }

            );
           routes.MapRoute(
             name: "BlogCategoryList",
             url: "blog/category/list/{basekey}/{smallkey}",
             defaults: new { controller = "Blog", action = "CategoryList",smallkey=UrlParameter.Optional }
         );
           routes.MapRoute(
                name: "GetCategoryList",
                url: "api/category/list",
                defaults: new { controller = "Blog", action = "GetCategoryList" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}