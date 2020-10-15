using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;

namespace TicketManagement.BLL.Infrastructure.Helpers
{
    public class DataTimeHelper : IDataTimeHelper
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
