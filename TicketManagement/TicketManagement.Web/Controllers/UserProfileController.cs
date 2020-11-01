using System.Threading.Tasks;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.UserProfile;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter(Order = 0)]
    [RedirectExceptionFilter]
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserService userService,
            IUserProfileService userProfileService)
        {
            this.userService = userService;
            this.userProfileService = userProfileService;
        }

        [HttpGet]
        [AjaxContentUrl]
        public ActionResult Index()
        {
            return this.PartialView(this.userProfileService.GetUserProfileViewModel(this.User.Identity.Name));
        }

        [HttpGet]
        [AjaxContentUrl]
        public ActionResult Edit()
        {
            return this.PartialView(this.userProfileService.GetEditUserProfileViewModel(this.User.Identity.Name));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserProfileViewModel userProfile)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView(userProfile);
            }

            await this.userProfileService.UpdateAsync(this.User.Identity.Name, userProfile);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [AjaxContentUrl]
        public ActionResult ChangePassword()
        {
            return this.PartialView(new UserPasswordViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(UserPasswordViewModel userPasswordModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView(userPasswordModel);
            }

            await this.userProfileService.ChangePasswordAsync(this.User.Identity.Name, userPasswordModel);

            return this.PartialView(userPasswordModel);
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<ActionResult> IncreaseBalance()
        {
            return this.PartialView(new BalanceViewModel());
        }

        [HttpPost]
        public ActionResult IncreaseBalance(BalanceViewModel balanceModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView(balanceModel);
            }

            this.userService.IncreaseBalance(balanceModel.Balance, this.User.Identity.Name);
            return this.PartialView("Index");
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult PurchaseHistory()
        {
            return this.PartialView(this.userProfileService.GetPurchaseHistoryViewModel(this.User.Identity.Name));
        }
    }
}