using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FruitPowerAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS for the specified origin, allowing all methods and headers
            var cors = new EnableCorsAttribute("http://localhost:5173", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services

            // Map attribute-based routes
            config.MapHttpAttributeRoutes();

            // Define default API route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
