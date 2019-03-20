using System.Web.Mvc;

namespace SamlSSO.Controllers
{
    public class LoggedInController : Controller
    {
        [Route("LoggedIn/GoodJob")]
        public ActionResult GoodJob()
        {
            return View();
        }
    }
}