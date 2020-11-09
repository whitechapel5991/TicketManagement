// ****************************************************************************
// <copyright file="UserProfileController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.Web.Filters.AcionFilters;
using TicketManagement.Web.Filters.ExceptionFilters;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.UserProfile;

namespace TicketManagement.Web.Controllers
{
    [Authorize]
    [UserProfileExceptionFilter]
    public class UserProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserProfileService userProfileService;

        public UserProfileController(
            IUserService userService,
            IUserProfileService userProfileService)
        {
            this.userService = userService;
            this.userProfileService = userProfileService;
        }

        [HttpGet]
        [AjaxContentUrl]
        public PartialViewResult Index()
        {
            return this.PartialView(this.userProfileService.GetUserProfileViewModel(this.User.Identity.Name));
        }

        [HttpGet]
        [AjaxContentUrl]
        public PartialViewResult Edit()
        {
            return this.PartialView(this.userProfileService.GetEditUserProfileViewModel(this.User.Identity.Name));
        }

        [HttpPost]
        public async Task<JsonResult> Edit(EditUserProfileViewModel userProfile)
        {
            await this.userProfileService.UpdateAsync(this.User.Identity.Name, userProfile);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", "UserProfile") });
        }

        [HttpGet]
        [AjaxContentUrl]
        public PartialViewResult ChangePassword()
        {
            return this.PartialView(new UserPasswordViewModel());
        }

        [HttpPost]
        public async Task<JsonResult> ChangePassword(UserPasswordViewModel userPasswordModel)
        {
            await this.userProfileService.ChangePasswordAsync(this.User.Identity.Name, userPasswordModel);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", "UserProfile") });
        }

        [HttpGet]
        public PartialViewResult IncreaseBalance()
        {
            return this.PartialView(new BalanceViewModel());
        }

        [HttpPost]
        public JsonResult IncreaseBalance(BalanceViewModel balanceModel)
        {
            this.userService.IncreaseBalance(balanceModel.Balance, this.User.Identity.Name);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", "UserProfile") });
        }

        [HttpGet]
        public PartialViewResult PurchaseHistory()
        {
            return this.PartialView(this.userProfileService.GetPurchaseHistoryViewModel(this.User.Identity.Name));
        }
    }
}