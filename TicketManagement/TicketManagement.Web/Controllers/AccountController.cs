using System.Threading.Tasks;
using System.Web.Mvc;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter(Order = 0)]
    [RedirectExceptionFilter]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [AjaxContentUrl]
        public ActionResult Login()
        {
            return this.PartialView(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView(model);
            }

            try
            {
                await this.accountService.SignInAsync(model.UserName, model.Password);

                return this.RedirectToAction("Events", "Event");
            }
            catch (UserNameOrPasswordWrongException ex)
            {
                this.ModelState.AddModelError("Login", ex.Message);
            }

            return this.PartialView(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            this.accountService.SignOut();

            return this.Json(new { success = true}, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [AjaxContentUrl]
        public ActionResult Register()
        {
            return this.PartialView(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView(model);
            }

            try
            {
                await this.accountService.RegisterUserAsync(model);

                return this.RedirectToAction("Login", "Account");
            }
            catch (RegisterUserWrongDataException ex)
            {
                this.ModelState.AddModelError("Register", ex.Message);
            }

            return this.PartialView(model);
        }
    }
}