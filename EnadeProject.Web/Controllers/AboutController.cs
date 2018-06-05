using System.Web.Mvc;

namespace EnadeProject.Web.Controllers
{
    public class AboutController : EnadeProjectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}