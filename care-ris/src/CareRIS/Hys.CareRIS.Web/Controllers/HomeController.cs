using System.Web.Mvc;
using Hys.CareRIS.Web.Security;

namespace Hys.CareRIS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.lang = Misc.GetLang(HttpContext, RouteData);
            return View("Index");
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}