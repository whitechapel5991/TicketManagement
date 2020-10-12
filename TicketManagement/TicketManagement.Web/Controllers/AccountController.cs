using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid) return this.View(model);

            try
            {
                this.accountService.SignIn(model.UserName, model.Password);

                return this.accountService.IsUserEventManager(this.User.Identity.GetUserId<int>()) ?
                    this.RedirectToAction("Index", new { area = "EventManager", controller = "Event" }) :
                    this.RedirectToAction("Events", "Event");
            }
            catch (UserNameOrPasswordWrongException ex)
            {
                this.ModelState.AddModelError("Login", ex.Message);
            }

            return this.View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            this.accountService.SignOut();

            return this.RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.accountService.RegisterUser(this.accountService.MapIdentityUser(model));

                return this.RedirectToAction("Login", "Account");
            }
            catch (RegisterUserWrongDataException ex)
            {
                this.ModelState.AddModelError("Register", ex.Message);
            }

            return this.View(model);
        }
    }
}