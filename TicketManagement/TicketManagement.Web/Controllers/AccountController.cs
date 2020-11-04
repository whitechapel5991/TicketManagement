using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [AccountExceptionFilter(Order = 0)]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [AjaxContentUrl]
        public PartialViewResult Login()
        {
            return this.PartialView(new LoginViewModel());
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            await this.accountService.SignInAsync(model.UserName, model.Password);
            return this.Json(new { success = true, returnContentUrl = this.Url.Action("Events", "Event", new { area = string.Empty }) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            this.accountService.SignOut();
            return this.Json(new { success = true, returnContentUrl = this.Url.Action("Login", "Account", new { area = string.Empty }) }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [AjaxContentUrl]
        public ActionResult Register()
        {
            return this.PartialView(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            await this.accountService.RegisterUserAsync(model);
            return this.Json(new { success = true, returnContentUrl = this.Url.Action("Login", "Account", new { area = string.Empty }) }, JsonRequestBehavior.AllowGet);
        }
    }
}