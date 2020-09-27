using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.Exceptions.OrderExceptions;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.ServiceValidators
{
    public class OrderValidator : IOrderValidator
    {
        public void SeatIsBlocked(EventSeat eventSeat)
        {
            if (eventSeat.State == EventSeatState.InBasket || eventSeat.State == EventSeatState.Sold)
            {
                throw new SeatCurrentlySoldOrBlocked($"Seat with row {eventSeat.Row} and number {eventSeat.Number} currently is not free.");
            }
        }

        public void Validate(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
