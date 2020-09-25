using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Models.Account;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        private IAuthenticationManager AuthenticationManager => this.HttpContext.GetOwinContext().Authentication;

        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    TicketManagementUser userDto = new TicketManagementUser() { UserName = model.UserName, Password = model.Password };
                    ClaimsIdentity claim = this.userService.Authenticate(userDto);
                    if (claim == default)
                    {
                        this.ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
                    }
                    else
                    {
                        this.AuthenticationManager.SignOut();
                        this.AuthenticationManager.SignIn(claim);

                        this.ViewBag.claim = claim;

                        if ((string)claim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value == "event manager")
                        {
                            return this.RedirectToAction("GetAllEvents", new { area = "EventManager", controller = "Event" });
                        }

                        return this.RedirectToAction("Events", "Event");
                    }
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Login", ex.Message);
                }
            }

            return this.View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

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
                return this.View();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = new TicketManagementUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        Language = model.Language.ToString(),
                        TimeZone = model.TimeZone,
                    };

                    this.userService.Create(user);
                    return this.RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Register", ex.Message);
                }
            }

            return this.View(model);
        }
    }
}