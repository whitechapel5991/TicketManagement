// ****************************************************************************
// <copyright file="ValidateHeaderAntiForgeryTokenAttribute.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TicketManagement.Web.Filters.AuthorizationFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateHeaderAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
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