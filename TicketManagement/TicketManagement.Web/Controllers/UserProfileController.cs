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
            var userOrders = this.orderService.GetHistoryOrdersById(user);
            var purchaseHistoryVM = this.MapFromListOrder(userOrders);

            UserProfileViewModel userView = new UserProfileViewModel
            {
                Balance = user.Balance,
                Email = user.Email,
                FirstName = user.FirstName,
                Surname = user.Surname,
                UserName = user.UserName,
                Language = (Language)Enum.Parse(typeof(Language), user.Language, true),
                TimeZone = user.TimeZone,
                PurchaseHistory = purchaseHistoryVM,
            };

            return this.View("Index", userView);
        }

        private PurchaseHistoryViewModel MapFromListOrder(List<Order> orderList)
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