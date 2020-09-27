using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.ServiceValidators.Base;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.ServiceValidators.Interfaces
{
    public interface IOrderValidator : IServiceValidator<Order>
    {
        void SeatIsBlocked(EventSeat eventSeat);
    }
}
