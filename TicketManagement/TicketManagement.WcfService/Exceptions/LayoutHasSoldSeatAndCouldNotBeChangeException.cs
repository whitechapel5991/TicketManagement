using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class LayoutHasSoldSeatAndCouldNotBeChangeException : WcfException
    {
        public LayoutHasSoldSeatAndCouldNotBeChangeException()
            : base(typeof(BLL.Exceptions.EventExceptions.LayoutHasSoldSeatAndCouldNotBeChangeException))
        {
        }
    }
}