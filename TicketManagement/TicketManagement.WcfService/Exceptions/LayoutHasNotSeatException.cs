using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class LayoutHasNotSeatException : WcfException
    {
        public LayoutHasNotSeatException()
            : base(typeof(BLL.Exceptions.EventExceptions.LayoutHasNotSeatException))
        {
        }
    }
}