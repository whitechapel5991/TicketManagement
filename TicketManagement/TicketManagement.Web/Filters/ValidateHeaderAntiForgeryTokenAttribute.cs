﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TicketManagement.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateHeaderAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("Filter context");
            }

            if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "POST")
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var httpContext = filterContext.HttpContext;
                    var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
                    AntiForgery.Validate(cookie?.Value, httpContext.Request.Headers["__RequestVerificationToken"]);
                }
            }
        }
    }
}