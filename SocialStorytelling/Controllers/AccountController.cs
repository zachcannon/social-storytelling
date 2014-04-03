using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace SocialStorytelling.Controllers
{
    public class AccountController : Controller
    {
        private string AuthorizedUserCookie = "TweetAuthCookie";
        private string ConsumerKey = "lgs8hg45HbaaLkkKFHDbzQ";
        private string ConsumerSecret = "JiaTz1ixrFdmFigEXalqZP2xL00c3EscVldyndvWI";

        public ActionResult Account()
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
                ViewBag.Username = Request.Cookies[AuthorizedUserCookie]["screen_name"];
            else
                ViewBag.Username = "No users logged in yet.";
            return View();
        }

        public ActionResult Authorize()
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
            {
                return RedirectToAction("Account");
            }
            else
            {
                TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);
                OAuthRequestToken requestToken = service.GetRequestToken("http://localhost:50114/Account/AuthorizeCallback");
                Uri uri = service.GetAuthorizationUri(requestToken);
                return new RedirectResult(uri.ToString(), false);
            }
        }

        public ActionResult AuthorizeCallback(string oauth_token, string oauth_verifier)
        {
            string returnVal;
            if (oauth_token == null || oauth_verifier == null)
            {
                returnVal = "Could not log in. OAuth returned null";
                return RedirectToAction("Account", new { username = returnVal });
            }
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);
            
            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());

            returnVal = string.Format("Your username is {0}", user.ScreenName);

            //Save into cookie
            HttpCookie userCookie = new HttpCookie(AuthorizedUserCookie);
            userCookie.Expires = DateTime.Now.AddDays(1);
            userCookie["consumer_key"] = ConsumerKey;
            userCookie["consumer_secret"] = ConsumerSecret;
            userCookie["oauth_token"] = oauth_token;
            userCookie["oauth_verifier"] = oauth_verifier;
            userCookie["screen_name"] = user.ScreenName;

            Response.Cookies.Add(userCookie);

            return RedirectToAction("Account");
        }

        public ActionResult LogOutAccount()
        {
            Response.Cookies[AuthorizedUserCookie].Expires = DateTime.Now.AddDays(-2);
            return RedirectToAction("Account");
        }
	}
}