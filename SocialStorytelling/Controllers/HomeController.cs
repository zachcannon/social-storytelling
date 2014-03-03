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
        ControllerBusiness controller = new ControllerBusiness();

        public ActionResult Index()
        {
            ViewBag.Message = "Message text here";
            return View();
        }

        public ActionResult GetStoryList()
        {
            List<Story> storybook = controller.GetStoryBook();
            return Json(storybook, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEntryList()
        {
            List<Entry> entryList = controller.GetEntryList();
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEntriesForGivenStory(int storyId)
        {
            List<Entry> entryList = controller.GetEntriesForGivenStory(storyId);
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPendingEntryList()
        {
            List<PendingEntry> pendingEntryList = controller.GetPendingEntryList();
            return Json(pendingEntryList, JsonRequestBehavior.AllowGet);
        }

        public void AddNewStory(string title, string prompt)
        {
            controller.AddNewStoryToBook(title, prompt);
        }
        
        public void AddNewEntry(string text, string author, int storyId)
        {
            controller.AddEntryToStory(storyId, author, text);
        }

        public void AddNewPendingEntry(string text, string author, int storyId)
        {
            controller.AddPendingEntryToList(storyId, author, text);
        }

        public void RemoveStory(int idToRemove)
        {
            controller.RemoveStoryFromBook(idToRemove);
        }
      
        public void RemoveEntry(int idToRemove)
        {
            controller.RemoveEntryFromList(idToRemove);
        }

        public void RemovePendingEntry(int idToRemove)
        {
            controller.RemovePendingEntryFromList(idToRemove);
        }

        public void PromotePendingEntry(int idToPromote)
        {
            controller.PromotePendingEntryFromList(idToPromote);
        }

    }
}