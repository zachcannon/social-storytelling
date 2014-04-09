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
        private string ConsumerKey = "qNLcTNRZVCYvzktylhw";
        private string ConsumerSecret = "6mbLcXOiaZT2kMMjdqxQ2CTrSsdbkJvpcGKrduoBxk";

        public ActionResult Authorize()
        {
            if (Request.Cookies[AuthorizedUserCookie] != null)
            {
                return RedirectToAction("Index", "Home");
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
            if (oauth_token == null || oauth_verifier == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);
            
            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());

            //Save into cookie
            HttpCookie userCookie = new HttpCookie(AuthorizedUserCookie);
            userCookie.Expires = DateTime.Now.AddHours(1);
            userCookie["access_token"] = accessToken.Token;
            userCookie["access_verifier"] = accessToken.TokenSecret;
            userCookie["screen_name"] = user.ScreenName;

            Response.Cookies.Add(userCookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOutAccount()
        {
            Response.Cookies[AuthorizedUserCookie].Expires = DateTime.Now.AddDays(-2);
            return RedirectToAction("Index", "Home");
        }
	}
}