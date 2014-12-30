namespace Socres.Web.Mvc.FilterAttributes
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Attribute for allowing different results for each culture.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class CultureBasedActionAttribute : ActionFilterAttribute
    {
        public const string LanguageUrlParameter = "language";
        public const string CultureUrlParameter = "culture";

        public string DefaultCulture { get; set; }
        public bool UseBrowserCulture { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureBasedActionAttribute"/> class.
        /// </summary>
        public CultureBasedActionAttribute()
        {
            UseBrowserCulture = true;    
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                var culture = GetCulture(controller);
                // if there is no culture yet in the url, redirect to a culture based action.
                if (string.IsNullOrEmpty(culture))
                {
                    culture = GetDefaultCulture(filterContext.RequestContext);
                    filterContext.Result = RedirectToCultureBasedAction(controller, culture);
                }
                else
                {
                    // if we have a culture in the url, set the currentthread to that culture.
                    SetThreadCulture(culture);
                }
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Gets the culture from the controller routedata.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>The culture in National Language Support format.</returns>
        private static string GetCulture(Controller controller)
        {
            var language = (string)controller.RouteData.Values[LanguageUrlParameter];
            var culture = (string)controller.RouteData.Values[CultureUrlParameter];

            return
                (string.IsNullOrEmpty(language) || string.IsNullOrEmpty(culture))
                    ? string.Empty
                    : string.Format("{0}-{1}", language, culture);
        }

        /// <summary>
        /// Gets the default culture.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns></returns>
        private string GetDefaultCulture(RequestContext requestContext)
        {
            // If a default culture has been supplied, use that.
            if (!string.IsNullOrEmpty(DefaultCulture))
            {
                return DefaultCulture;
            }

            // If we have a culture from the browser, and we can use it, use that.
            var languages = requestContext.HttpContext.Request.UserLanguages;
            if (UseBrowserCulture && languages != null && languages.Any())
            {
                return languages.First();
            }

            // Otherwise use the culture from the current Thread.
            return Thread.CurrentThread.CurrentUICulture.Name;
        }

        /// <summary>
        /// Redirects the index of to culture based.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        private static ActionResult RedirectToCultureBasedAction(Controller controller, string culture)
        {
            var splitCulture = culture.Split('-');

            controller.RouteData.Values[LanguageUrlParameter] = splitCulture[0];
            controller.RouteData.Values[CultureUrlParameter] = splitCulture[1];

            return new RedirectToRouteResult(controller.RouteData.Values);
        }

        /// <summary>
        /// Sets the thread culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        private static void SetThreadCulture(string culture)
        {
            try
            {
                var cultureInfo = CultureInfo.GetCultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            catch (Exception ex)
            {
                Trace.TraceError(
                    string.Format("Error setting ThreadCulture in CultureBasedActionAttribute: {0}",
                        ex.Message));
            }
        }
    }
}