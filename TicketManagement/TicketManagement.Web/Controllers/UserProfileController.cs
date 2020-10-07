using System.Web.Mvc;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.UserProfile;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter]
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

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(this.userProfileService.GetUserProfileViewModel(this.User.Identity.Name));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            return this.View(this.userProfileService.GetEditUserProfileViewModel(this.User.Identity.Name));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditUserProfileViewModel userProfile)
        {
            if (!this.ModelState.IsValid) return this.View(userProfile);

            this.userProfileService.Update(this.User.Identity.Name, userProfile);
            return this.RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return this.View(new UserPasswordViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(UserPasswordViewModel userPasswordModel)
        {
            if (!this.ModelState.IsValid) return this.View(userPasswordModel);

            this.userProfileService.ChangePassword(this.User.Identity.Name, userPasswordModel);

            return this.View(userPasswordModel);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult IncreaseBalance()
        {
            return this.View(this.userProfileService.GetBalanceViewModel(this.User.Identity.Name));
        }

        [Authorize]
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