﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialStorytelling.Business;

namespace SocialStorytelling.Controllers
{
    public class AdminController : Controller
    {
        private string AuthorizedUserCookie = "TweetAuthCookie";
        SocialStorytellingService service = new SocialStorytellingService();


        public ActionResult AdminView()
        {            
            return View();
        }

        //----------------------Home Page Actions------------------------

        [HttpPost]
        public ActionResult AddNewStory(string title, string prompt)
        {
            string username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
            string access_token = Request.Cookies[AuthorizedUserCookie]["access_token"];
            string access_verifier = Request.Cookies[AuthorizedUserCookie]["access_verifier"];
            service.AddNewStoryToBook(title, prompt, access_token, access_verifier);
            return RedirectToAction("AdminView");            
        }

        [HttpPost]
        public ActionResult CloseStory(int idToClose)
        {
            service.CloseAStory(idToClose);
            return RedirectToAction("AdminView");
        }

        [HttpPost]
        public ActionResult RemoveStory(int idToRemove)
        {
            service.RemoveStoryFromBook(idToRemove);
            return RedirectToAction("AdminView");
        } 

        //--------------------View Story Page Actions---------------------------

        [HttpPost]
        public ActionResult PromotePendingEntry(int idToPromote)
        {
            service.PromotePendingEntryFromList(idToPromote);
            return RedirectToAction("AdminView");
        }


        [HttpPost]
        public ActionResult PromoteHighestPendingEntry(int idToPromote)
        {
            service.PromoteMostPopularPendingEntry(idToPromote);
            return RedirectToAction("AdminView");
        }

        [HttpPost]
        public ActionResult AddNewEntry(string text, int storyId)
        {
            string username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
            service.AddEntryToStory(storyId, username, text);
            return RedirectToAction("AdminView");
        }


        [HttpPost]
        public ActionResult RemovePendingEntry(int idToRemove)
        {
            service.RemovePendingEntryFromList(idToRemove);
            return RedirectToAction("AdminView");
        }

        [HttpPost]
        public ActionResult RemoveEntry(int idToRemove)
        {
            service.RemoveEntryFromList(idToRemove);
            return RedirectToAction("AdminView");
        }
	}
}