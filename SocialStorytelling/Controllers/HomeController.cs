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
        SocialStorytellingService service = new SocialStorytellingService();

        public ActionResult Index()
        {
            ViewBag.Message = "Message text here";
            return View();
        }

        public ActionResult GetStoryList()
        {
            List<Story> storybook = service.GetStoryBook();
            return Json(storybook, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEntryList()
        {
            List<Entry> entryList = service.GetEntryList();
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEntriesForGivenStory(int storyId)
        {
            List<Entry> entryList = service.GetEntriesForGivenStory(storyId);
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPendingEntryList()
        {
            List<PendingEntry> pendingEntryList = service.GetPendingEntryList();
            return Json(pendingEntryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegisterNewUser(string username, string password)
        {
            string returnValue = service.RegisterNewUser(username, password);
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        public void AddNewStory(string title, string prompt)
        {
            service.AddNewStoryToBook(title, prompt);
        }
        
        public void AddNewEntry(string text, string author, int storyId)
        {
            service.AddEntryToStory(storyId, author, text);
        }

        public void AddNewPendingEntry(string text, string author, int storyId)
        {
            service.AddPendingEntryToList(storyId, author, text);
        }

        public void RemoveStory(int idToRemove)
        {
            service.RemoveStoryFromBook(idToRemove);
        }
      
        public void RemoveEntry(int idToRemove)
        {
            service.RemoveEntryFromList(idToRemove);
        }

        public void RemovePendingEntry(int idToRemove)
        {
            service.RemovePendingEntryFromList(idToRemove);
        }

        public void PromotePendingEntry(int idToPromote)
        {
            service.PromotePendingEntryFromList(idToPromote);
        }

    }
}