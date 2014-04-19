using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialStorytelling.Business;

namespace SocialStorytelling.Controllers
{
    public class HomeController : Controller
    {
        private string AuthorizedUserCookie = "TweetAuthCookie";
        SocialStorytellingService service = new SocialStorytellingService();

        public ActionResult Index()
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
                ViewBag.Username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
            else
                ViewBag.Username = "No users logged in yet.";
            return View();
        }

        [HttpGet]
        public ActionResult GetStoryList()
        {
            List<Story> storybook = service.GetStoryBook();
            return Json(storybook, JsonRequestBehavior.AllowGet);
        }        

        [HttpPost]
        public ActionResult GoToStoryViewForStory(int theStoryId)
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
            {
                return RedirectToAction("StoryView", "StoryView", new { storyIdToView = theStoryId });
            }
            else
                return RedirectToAction("Authorize", "Account");
        }
        
    }
}