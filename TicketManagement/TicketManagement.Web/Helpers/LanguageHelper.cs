using System;
using System.Web.Mvc;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Helpers
{
    public static class LanguageHelper
    {
        public static MvcHtmlString LanguagesList(this HtmlHelper html)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", $"dropdown-menu");

            foreach (var lang in Enum.GetValues(typeof(Language)))
            {
                var a = new TagBuilder("a")
                {
                    InnerHtml = $"<a class='dropdown-item' href='/Language/ChangeLanguage'>{lang}</a>"
                };

                div.InnerHtml += a.ToString();
            }

            return new MvcHtmlString(div.ToString());
        }
    }
}