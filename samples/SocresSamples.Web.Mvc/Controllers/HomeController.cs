using System.Web.Mvc;

namespace SocresSamples.Web.Mvc.Controllers
{
    using Socres.Web.Mvc.FilterAttributes;
    using SocresSamples.Web.Mvc.Properties;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CultureBasedAction]
        public ActionResult About()
        {
            ViewBag.Message = Resources.Home_About_Message;

            return View();
        }
    }
}