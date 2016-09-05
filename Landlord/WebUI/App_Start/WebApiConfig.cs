using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetNearlyAreas",
                routeTemplate: "api/{controller}/{action}/{latit}/{longit}"//,
                //defaults: new { x = 0, y = 0 }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
