using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialStorytelling.Business;

namespace SocialStorytelling.Controllers
{
    public class StoryViewController : Controller
    {
        private string AuthorizedUserCookie = "TweetAuthCookie";
        private string RecentStoryCookie = "RecentStory";
        SocialStorytellingService service = new SocialStorytellingService();


        public ActionResult StoryView(int storyIdToView = -1)
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
                ViewBag.Username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
            else
                ViewBag.Username = "No users logged in yet.";

            if (storyIdToView == -1)
            {
                HttpCookie myCookie = HttpContext.Request.Cookies[RecentStoryCookie];               
                if (myCookie != null)
                    storyIdToView = Convert.ToInt32(myCookie["recentStoryId"]);
            }

            if (storyIdToView == -1)
                return RedirectToAction("Index", "Home");

            if (!service.IsStoryInBook(storyIdToView))
                return RedirectToAction("Index", "Home");

            HttpCookie mostRecentStory = new HttpCookie(RecentStoryCookie);
            mostRecentStory["recentStoryId"] = storyIdToView.ToString();
            Response.Cookies.Add(mostRecentStory);

            ViewBag.CurrentStoryStatus = service.GetSpecificStoryStatus(storyIdToView);

            return View();
        }

        // -------------------------GET REQUESTS TO DISPLAY DATABASE INFO---------------------

        

        [HttpGet]
        public ActionResult GetEntriesForGivenStory()
        {
            string storyId = Request.Cookies[RecentStoryCookie]["recentStoryId"];

            List<Entry> entryList = service.GetEntriesForGivenStory(Convert.ToInt32(storyId));
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPendingEntryListForGivenStory()
        {
            string storyId = Request.Cookies[RecentStoryCookie]["recentStoryId"];

            List<PendingEntry> pendingEntryList = service.GetPendingEntryListForGivenStory(Convert.ToInt32(storyId));
            return Json(pendingEntryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEntryList()
        {
            List<Entry> entryList = service.GetEntryList();
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPendingEntryList()
        {
            
            List<PendingEntry> pendingEntryList = service.GetPendingEntryList();
            pendingEntryList = pendingEntryList.OrderBy(list => list.StoryId).ToList();
            return Json(pendingEntryList, JsonRequestBehavior.AllowGet);
        }

        // --------------------------------USER ACTIONS----------------------------------

        [HttpPost]
        public ActionResult AddNewPendingEntry(string text)
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
            {
                string username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
                string access_token = Request.Cookies[AuthorizedUserCookie]["access_token"];
                string access_verifier = Request.Cookies[AuthorizedUserCookie]["access_verifier"];
                int storyId = Convert.ToInt32(Request.Cookies[RecentStoryCookie]["recentStoryId"]);
                service.AddPendingEntryToList(storyId, username, text, access_token, access_verifier);
                return RedirectToAction("StoryView");
            }
            else
                return RedirectToAction("Authorize", "Account");
        }

        [HttpPost]
        public ActionResult VoteForPendingEntry(int pendingEntryId)
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
            {
                string username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
                string access_token = Request.Cookies[AuthorizedUserCookie]["access_token"];
                string access_verifier = Request.Cookies[AuthorizedUserCookie]["access_verifier"];
                service.VoteForPendingEntry(pendingEntryId, username, access_token, access_verifier);
                return RedirectToAction("StoryView");
            }
            else
                return RedirectToAction("Authorize", "Account");
        }
	}
}