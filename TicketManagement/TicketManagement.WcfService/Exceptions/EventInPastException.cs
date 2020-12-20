using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class EventInPastException : WcfException
    {
        public EventInPastException()
            : base(typeof(BLL.Exceptions.EventExceptions.EventInPastException))
        {
        }
    }
}