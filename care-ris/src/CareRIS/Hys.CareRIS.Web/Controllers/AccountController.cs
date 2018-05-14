using System.Web.Mvc;

namespace Hys.CareRIS.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Methods
        
        public ActionResult LogOn()
        {
            return View();
        }
       
        public ActionResult LogOut()
        {
            return RedirectToAction("LogOn", "Account");
        }
        #endregion
    }
}