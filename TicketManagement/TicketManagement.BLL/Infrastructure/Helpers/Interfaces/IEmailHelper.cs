using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.BLL.Infrastructure.Helpers.Interfaces
{
    public interface IEmailHelper
    {
        void SendEmail(string recipient, string subject, string message);
    }
}
