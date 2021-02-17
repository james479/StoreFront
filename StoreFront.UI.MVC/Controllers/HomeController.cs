using System.Web.Mvc;
using StoreFront.DATA.EF;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.MaskSizes.ToList());
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
