using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class NotEnoughMoneyException : WcfException
    {
        public NotEnoughMoneyException()
            : base(typeof(BLL.Exceptions.OrderExceptions.NotEnoughMoneyException))
        {
        }
    }
}