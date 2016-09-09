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

            //config.Routes.MapHttpRoute(
            //    name: "SetStartTime",
            //    routeTemplate: "api/{controller}/{action}/{areaId}/{startTime}"
            //    );

            config.Routes.MapHttpRoute(
              name: "GetNearlyAreas",
              routeTemplate: "api/{controller}/{action}/{latit}/{longit}"//,
                                                                         // defaults: new { x = (string)null, y = (string)null }
              );

            config.Routes.MapHttpRoute(
                name: "AddOrUpdateArea",
                routeTemplate: "api/{controller}/{action}/{areaStr}/{latit}/{longit}"
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
