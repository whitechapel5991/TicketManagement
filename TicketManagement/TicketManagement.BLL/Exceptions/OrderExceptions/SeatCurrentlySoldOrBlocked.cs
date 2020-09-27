using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.BLL.Exceptions.OrderExceptions
{
    public class SeatCurrentlySoldOrBlocked : Exception
    {
        public SeatCurrentlySoldOrBlocked(string message)
            : base(message)
        {
        }
    }
}
