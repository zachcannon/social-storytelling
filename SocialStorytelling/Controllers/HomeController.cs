using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialStorytelling.Business;
using SocialStorytelling.Data;
using SocialStorytelling.Models;

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
            StoryModel story = new StoryModel(1, "TestStory", "TestPrompt");
            return Json(story, JsonRequestBehavior.AllowGet);
        }

    }
}