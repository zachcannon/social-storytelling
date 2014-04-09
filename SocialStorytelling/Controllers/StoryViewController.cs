using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialStorytelling.Controllers
{
    public class StoryViewController : Controller
    {
        private string AuthorizedUserCookie = "TweetAuthCookie";

        public ActionResult StoryView()
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
                ViewBag.Username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
            else
                ViewBag.Username = "No users logged in yet.";
            return View();
        }
	}
}