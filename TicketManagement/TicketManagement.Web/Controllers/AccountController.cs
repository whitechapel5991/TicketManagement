// ****************************************************************************
// <copyright file="AccountController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using System.Web.Mvc;
using TicketManagement.Web.Filters.ActionFilters;
using TicketManagement.Web.Filters.AuthorizationFilters;
using TicketManagement.Web.Filters.ExceptionFilters;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;
using TicketManagement.Web.WcfInfrastructure;

namespace TicketManagement.Web.Controllers
{
    [AccountExceptionFilter]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        [AjaxContentUrl]
        public PartialViewResult Login()
        {
            return this.PartialView(new LoginViewModel());
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public JsonResult Login(LoginViewModel model)
        {
            var token = this.accountService.SignIn(model.UserName, model.Password);
            if (!string.IsNullOrEmpty(token))
            {
                this.HttpContext.Response.Cookies.Remove("access_token");
                var cookie = new System.Web.HttpCookie("access_token", token)
                {
                    HttpOnly = false,
                };
                this.Response.Cookies.Add(cookie);
                CredentialsContainer.Token = token;
            }

            return this.Json(new { returnContentUrl = this.Url.Action("Events", "Event") });
        }

        [Authorize]
        [HttpPost]
        public JsonResult Logout()
        {
            this.accountService.SignOut();
            //this.HttpContext.Response.Cookies.Remove("access_token");
            return this.Json(new { returnContentUrl = this.Url.Action("Login", "Account") });
        }

        [HttpGet]
        [AllowAnonymous]
        [AjaxContentUrl]
        public PartialViewResult Register()
        {
            return this.PartialView(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public JsonResult Register(RegisterViewModel model)
        {
            this.accountService.RegisterUser(model);
            return this.Json(new { returnContentUrl = this.Url.Action("Login", "Account") });
        }
    }
}