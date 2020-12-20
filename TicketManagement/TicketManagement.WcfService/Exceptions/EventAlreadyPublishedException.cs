using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class EventAlreadyPublishedException : WcfException
    {
        public EventAlreadyPublishedException()
            : base(typeof(BLL.Exceptions.EventExceptions.EventAlreadyPublishedException))
        {
        }
    }
}