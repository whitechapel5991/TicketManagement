using System;
using System.Globalization;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TicketManagement.Web.Constants;
using TicketManagement.Web.Filters;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter(Order = 0)]
    [AllowAnonymous]
    [RedirectExceptionFilter]
    public class LanguageController : Controller
    {
        private const string CookieLangName = "lang";

        [HttpPost]
        public ActionResult ChangeLanguage(Language language)
        {
            var languageString = language.ToString();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageString);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(languageString);

            var languageCookie = this.Request.Cookies[CookieLangName];
            if (languageCookie != null)
            {
                languageCookie.Value = languageString;
            }
            else
            {
                languageCookie = new HttpCookie(CookieLangName)
                {
                    HttpOnly = false,
                    Value = languageString,
                    Expires = DateTime.Now.AddYears(1),
                };
            }

            this.Response.Cookies.Add(languageCookie);
            this.Response.StatusCode = (int)HttpStatusCode.OK;

            return this.Json(new { success = true, redirectUrl = Url.Action("Index", "StartApp"), updateContentUrl = AjaxContentUrlAttribute.CurrentContentUrl }, JsonRequestBehavior.AllowGet);
        }
    }
}