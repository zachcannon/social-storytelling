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

        public ActionResult GetEntryList()
        {
            ControllerBusiness controller = new ControllerBusiness();
            List<Entry> entryList = controller.GetEntryList();
            return Json(entryList, JsonRequestBehavior.AllowGet);
        }

        public void AddNewStory(string title, string prompt)
        {
            ControllerBusiness controller = new ControllerBusiness();
            controller.AddNewStoryToBook(title, prompt);
        }
        public void AddNewEntry(string text, string author, int storyId)
        {
            ControllerBusiness controller = new ControllerBusiness();
            controller.AddEntryToStory(storyId, author, text);
        }

        public void RemoveStory(int idToRemove)
        {
            ControllerBusiness controller = new ControllerBusiness();
            controller.RemoveStoryFromBook(idToRemove);
        }
      
        public void RemoveEntry(int idToRemove)
        {
            ControllerBusiness controller = new ControllerBusiness();
            controller.RemoveEntryFromList(idToRemove);
        }

    }
}