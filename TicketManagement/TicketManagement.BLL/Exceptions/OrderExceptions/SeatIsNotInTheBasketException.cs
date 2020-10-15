using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
