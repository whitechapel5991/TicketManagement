using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Infrastructure.Helpers.Interfaces
{
    public interface IHtmlHelper
    {
        string GetEventHtml(EventSeat eventSeat, EventArea eventArea, Event @event, Layout layout);
    }
}
