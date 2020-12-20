using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class SomeAreaHasNotPriceException : WcfException
    {
        public SomeAreaHasNotPriceException()
            : base(typeof(BLL.Exceptions.EventExceptions.SomeAreaHasNotPriceException))
        {
        }
    }
}