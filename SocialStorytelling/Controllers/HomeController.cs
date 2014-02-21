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

        public ActionResult GetTheStory()
        {
            return Json("Story goes here", JsonRequestBehavior.AllowGet);
        }

    }
}