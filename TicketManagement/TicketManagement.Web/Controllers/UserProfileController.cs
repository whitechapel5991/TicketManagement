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
        public ActionResult Index()
        {
            return this.View(this.userProfileService.GetUserProfileViewModel(this.User.Identity.Name));
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return this.View(this.userProfileService.GetEditUserProfileViewModel(this.User.Identity.Name));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserProfileViewModel userProfile)
        {
            if (!this.ModelState.IsValid) return this.View(userProfile);

            await this.userProfileService.UpdateAsync(this.User.Identity.Name, userProfile);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return this.View(new UserPasswordViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(UserPasswordViewModel userPasswordModel)
        {
            if (!this.ModelState.IsValid) return this.View(userPasswordModel);

            await this.userProfileService.ChangePasswordAsync(this.User.Identity.Name, userPasswordModel);

            return this.View(userPasswordModel);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> IncreaseBalance()
        {
            return this.View(await this.userProfileService.GetBalanceViewModelAsync(this.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult IncreaseBalance(BalanceViewModel balanceModel)
        {
            if (!this.ModelState.IsValid) return this.View(balanceModel);

            this.userService.IncreaseBalance(balanceModel.Balance, this.User.Identity.Name);
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult PurchaseHistory()
        {
            return this.View(this.userProfileService.GetPurchaseHistoryViewModel(this.User.Identity.Name));
        }
    }
}