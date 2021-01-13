using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Routting to the addSignature funtion in the Causes controller 
            routes.MapRoute(
                name: "addSignature",
                url: "CauseDetailsPage/addSignature",
                defaults: new { controller = "Causes", action = "addSignature" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
