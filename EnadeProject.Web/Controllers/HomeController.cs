using System.Web.Mvc;

namespace EnadeProject.Web.Controllers
{
    public class HomeController : EnadeProjectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}