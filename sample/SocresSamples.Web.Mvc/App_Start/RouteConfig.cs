using System.Web.Mvc;
using System.Web.Routing;

namespace SocresSamples.Web.Mvc
{
    using Socres.Web.Mvc.FilterAttributes;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "DefaultInternationalization",
                "{" + CultureBasedActionAttribute.LanguageUrlParameter + 
                "}-{" + CultureBasedActionAttribute.CultureUrlParameter + "}/{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
