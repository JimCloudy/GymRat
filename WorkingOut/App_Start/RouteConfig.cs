using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WorkingOut
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Create",
                url: "Workout/Create/{year}/{month}/{day}",
                defaults: new { controller = "Workout", action = "Create" },
                constraints: new {year = @"^2[\d]{3}$", month = @"^1[0-2]{0,1}$|^0[1-9]$|^[2-9]$", day = @"^0[1-9]{1}$|^[1-2]{1}[\d]{1}$|^3[0-1]{1}$" }
                );

            routes.MapRoute(
                name: "Calendar",
                url: "Workout/{year}/{month}",
                defaults: new { controller = "Workout", action = "Index" },
                constraints: new { year = @"^2[\d]{3}$", month = @"^1[0-2]{0,1}$|^0[1-9]$|^[2-9]$" }
                ); 
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
