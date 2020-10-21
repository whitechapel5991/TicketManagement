using System;

namespace TicketManagement.BLL.Exceptions.OrderExceptions
{
    public class SeatIsNotInTheBasketException: Exception
    {
        public SeatIsNotInTheBasketException(string message)
            : base(message)
        {
        }
    }
}
