using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Models.Cart;

namespace TicketManagement.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IEventSeatService eventSeatService;

        public CartController(IUserService userService, IOrderService orderService)
        {
            this.userService = userService;
            this.orderService = orderService;
        }

        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            var userCartOrders = this.orderService.GetCartOrdersByName(this.User.Identity.Name);

            return this.View(this.MapToCartViewModel(userCartOrders));
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult Buy(int? orderId)
        {
            this.orderService.Buy(orderId.Value);

            return this.View("Index");
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult DeleteFromCart(int? orderId)
        {
            this.orderService.DeleteFromCart(orderId.Value);

            return this.View("Index");
        }

        private CartViewModel MapToCartViewModel(List<Order> userCartOrders)
        {
            var purchaseHistoryVM = new CartViewModel()
            {
                Orders = new List<OrderViewModel>(),
            };

            foreach (Order order in userCartOrders)
            {
                var @event = this.eventSeatService.GetEventByEventSeatId(order.EventSeatId);

                var orderVm = new OrderViewModel
                {
                    OrderId = order.Id,
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