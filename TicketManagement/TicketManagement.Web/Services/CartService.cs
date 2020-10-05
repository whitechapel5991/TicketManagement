using System.Collections.Generic;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Cart;

namespace TicketManagement.Web.Services
{
    internal class CartService : ICartService
    {
        private readonly IOrderService orderService;
        private readonly IEventSeatService eventSeatService;

        public CartService(IOrderService orderService, IEventSeatService eventSeatService)
        {
            this.orderService = orderService;
            this.eventSeatService = eventSeatService;
        }

        public void Buy(int orderId)
        {
            this.orderService.Buy(orderId);
        }

        public void Delete(int orderId)
        {
            this.orderService.DeleteFromCart(orderId);
        }

        public CartViewModel GetCartViewModelByUserName(string userName)
        {
            return this.MapToCartViewModel(this.orderService.GetCartOrdersByName(userName));
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
                    TicketCost = this.eventSeatService.GetSeatCost(order.EventSeatId),
                    EventName = @event.Name,
                    EventDescription = @event.Description,
                };

                purchaseHistoryVM.Orders.Add(orderVm);
            }

            return purchaseHistoryVM;
        }
    }
}