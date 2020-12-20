using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class EventExistInTheLayoutInThisTimeException : WcfException
    {
        public EventExistInTheLayoutInThisTimeException()
            : base(typeof(BLL.Exceptions.EventExceptions.EventExistInTheLayoutInThisTimeException))
        {
        }
    }
}