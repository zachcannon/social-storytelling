using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialStorytelling.Business;
using SocialStorytelling.Data;

namespace SocialStorytelling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Message text here";
            return View();
        }

        public ActionResult GetStoryList()
        {
            ControllerBusiness controller = new ControllerBusiness();
            List<Story> storybook = controller.GetStoryBook();
            return Json(storybook, JsonRequestBehavior.AllowGet);
        }

    }
}