using System.Web.Mvc;

namespace Hys.CareRIS.Web.Controllers
{
    public class FileUploadController : Controller
    {
        public ActionResult Remove(string[] fileNames)
        {
            // Don't delete the physicscal file , keep the same as client agent
            return Content("");
        }
    }

}