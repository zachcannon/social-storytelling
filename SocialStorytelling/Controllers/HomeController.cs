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
        public ActionResult AddNewStory(string title, string prompt)
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
            {
                string username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
                string access_token = Request.Cookies[AuthorizedUserCookie]["access_token"];
                string access_verifier = Request.Cookies[AuthorizedUserCookie]["access_verifier"];
                service.AddNewStoryToBook(title, prompt, access_token, access_verifier);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Authorize", "Account");
        }
        
        
        //--------------ADMIN COMMANDS----------------

        

        [HttpPost]
        public ActionResult RemoveStory(int idToRemove)
        {
            service.RemoveStoryFromBook(idToRemove);
            return RedirectToAction("Index");
        }        

        [HttpPost]
        public ActionResult CloseStory(int idToClose)
        {
            service.CloseAStory(idToClose);
            return RedirectToAction("Index");
        }
    }
}