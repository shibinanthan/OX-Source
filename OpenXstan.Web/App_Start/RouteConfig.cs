using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OpenXstan.Web.Filters;

namespace OpenXstan.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            //routes.MapRoute(
            //   name: "ReportRoute",
            //   url: "rep/{controller}/{action}/{category}",
            //   defaults: new { controller = "ProductReport", action = "List" },
            //   constraints: new { category = @"^[a-zA-Z]*$" }
            //);

            //routes.MapRoute(
            //  name: "SearchRoute",
            //  url: "Search",
            //  defaults: new { controller = "Search", action = "Index" }
            //);

            //routes.MapRoute(
            //   name: "SearchRoute1",
            //   url: "{query}/{page}",
            //   defaults: new { controller = "Search", action = "Result" },
            //   constraints: new { query = @"^[a-zA-Z]*$", page = @"\d{1,2}" }
            //);

            //routes.MapRoute(
            //name: "SearchRoute2",
            //url: "{controller}/{action}/{query}/{page}",
            //defaults: new { controller = "Search", action = "Result", query = "test", page = 5 },
            //constraints: new { query = @"^[a-zA-Z]*$", page = @"\d{1,2}" }
            //);

           // routes.MapRoute(
           //    name: "WebApi",
           //    url: "Api/{controller}/{id}",
           //    defaults: new { id = UrlParameter.Optional }
           //);

            routes.MapRoute(
            name: "ProductRoute2",
            url: "{id}",
            defaults: new { controller = "Product", action = "Country", id = UrlParameter.Optional },
            constraints: new { id = "(us|ca)" }
            );

            routes.MapRoute(
            name: "ProductRoute",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
            constraints: new { id = new CustomConstraint() }
            );

            routes.MapRoute(
            name: "ProductRoute1",
            url: "{controller}/{action}/test/{id}",
            defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            ).RouteHandler = new CustomRouteHandler();

          //  routes.MapRoute(
          //    name: "ProductRoute1",
          //    url: "{controller}/{action}/{*query}",
          //    defaults: new { controller = "Product", action = "Index" }
          //);
        }
    }
}