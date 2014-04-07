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

        [HttpGet]
        public ActionResult GetEntryList()
        {
            List<Entry> entryList = service.GetEntryList();
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEntriesForGivenStory(int storyId)
        {
            List<Entry> entryList = service.GetEntriesForGivenStory(storyId);
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPendingEntryList()
        {
            List<PendingEntry> pendingEntryList = service.GetPendingEntryList();
            return Json(pendingEntryList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNewStory(string title, string prompt)
        {
            service.AddNewStoryToBook(title, prompt);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult AddNewEntry(string text, string author, int storyId)
        {
            service.AddEntryToStory(storyId, author, text);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddNewPendingEntry(string text, string author, int storyId)
        {
            service.AddPendingEntryToList(storyId, author, text);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveStory(int idToRemove)
        {
            service.RemoveStoryFromBook(idToRemove);
            return RedirectToAction("Index");
        }
      
        [HttpPost]
        public ActionResult RemoveEntry(int idToRemove)
        {
            service.RemoveEntryFromList(idToRemove);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemovePendingEntry(int idToRemove)
        {
            service.RemovePendingEntryFromList(idToRemove);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PromotePendingEntry(int idToPromote)
        {
            service.PromotePendingEntryFromList(idToPromote);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult VoteForPendingEntry(int pendingEntryId)
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
            {
                string username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
                service.VoteForPendingEntry(pendingEntryId, username);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Account", "Account");
        }

    }
}