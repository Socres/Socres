using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Azure.Search.Models;
using Microsoft.Rest.Azure;

namespace SocresSamples.Azure.Search
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                AzureSearchConfig.EnsureSearchIndex().Wait();
            }
            catch (AggregateException ex)
            {
                var cloudEx = ex.InnerException as CloudException;
            }
        }
    }
}
