using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Controllers
{
    public class LanguageController : Controller
    {
        private const string CookieLangName = "lang";
        // GET: Language
        [HttpPost]
        public void ChangeLanguage(Language language)
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
                    Expires = DateTime.Now.AddYears(1)
                };
            }

            this.Response.Cookies.Add(languageCookie);
        }
    }
}