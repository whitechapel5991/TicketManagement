using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.Web.Constants;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Models.UserProfile;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter]
    public class UserProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IEventSeatService eventSeatService;

        public UserProfileController(IUserService userService, IOrderService orderService, IEventSeatService eventSeatService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.eventSeatService = eventSeatService;
        }

        [Authorize]
        public ActionResult Index()
        {
            TicketManagementUser user = this.userService.FindByName(this.User.Identity.Name);

            UserProfileViewModel userView = new UserProfileViewModel
            {
                Balance = user.Balance,
                Email = user.Email,
                FirstName = user.FirstName,
                Surname = user.Surname,
                UserName = user.UserName,
                Language = (Language)Enum.Parse(typeof(Language), user.Language, true),
                TimeZone = user.TimeZone,
            };

            return this.View(userView);
        }

        [Authorize]
        public ActionResult Edit()
        {
            TicketManagementUser user = this.userService.FindByName(this.User.Identity.Name);

            var editUserProfile = new EditUserProfileViewModel
            {
                FirstName = user.FirstName,
                Surname = user.Surname,
                Language = (Language)Enum.Parse(typeof(Language), user.Language, true),
                TimeZone = user.TimeZone,
                Email = user.Email,
            };

            return this.View(editUserProfile);
        }

        [HttpPost]
        public ActionResult Edit(EditUserProfileViewModel userProfile)
        {
            if (this.ModelState.IsValid)
            {
                TicketManagementUser user = this.userService.FindByName(this.User.Identity.Name);

                user.FirstName = userProfile.FirstName;
                user.Surname = userProfile.Surname;
                user.Language = userProfile.Language.ToString();
                user.TimeZone = userProfile.TimeZone;
                user.Email = userProfile.Email;

                this.userService.Update(user);
                return this.RedirectToAction("Index");
            }
               

            return this.View(userProfile);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return this.View(new UserPasswordViewModel());
        }

        [Authorize()]
        [HttpPost]
        public ActionResult ChangePassword(UserPasswordViewModel userPasswordModel)
        {
            if (this.ModelState.IsValid)
            {
                this.userService.ChangePassword(userPasswordModel.OldPassword, userPasswordModel.Password, this.User.Identity.Name);
            }

            return this.View(userPasswordModel);
        }

        [Authorize(Roles = "user")]
        public ActionResult IncreaseBalance()
        {
            TicketManagementUser user = this.userService.FindByName(this.User.Identity.Name);

            var balanceModel = new BalanceViewModel
            {
                Balance = user.Balance,
            };

            return this.View(balanceModel);
        }

        [HttpPost]
        public ActionResult IncreaseBalance(BalanceViewModel balanceModel)
        {
            if (this.ModelState.IsValid)
            {
                this.userService.IncreaseBalance(balanceModel.Balance, this.User.Identity.Name);
                return this.RedirectToAction("Index");
            } 

            return this.View(balanceModel);
        }

        [Authorize(Roles = "user")]
        public ActionResult PurchaseHistory()
        {
            var userOrders = this.orderService.GetHistoryOrdersByName(this.User.Identity.Name);

            return this.View(this.MapToPurchaseHistoryViewModel(userOrders));
        }

        private PurchaseHistoryViewModel MapToPurchaseHistoryViewModel(List<Order> orderList)
        {
            var purchaseHistoryVM = new PurchaseHistoryViewModel()
            {
                Orders = new List<OrderViewModel>(),
            };

            foreach (Order order in orderList)
            {
                var @event = this.eventSeatService.GetEventByEventSeatId(order.EventSeatId);

                var orderVm = new OrderViewModel
                {
                    DatePurchase = order.Date,
                    EventCost = this.eventSeatService.GetSeatCost(order.EventSeatId),
                    EventName = @event.Name,
                    EventDescription = @event.Description,
                };

                purchaseHistoryVM.Orders.Add(orderVm);
            }

            return purchaseHistoryVM;
        }
    }
}