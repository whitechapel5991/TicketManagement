using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class SeatIsNotInTheBasketException : WcfException
    {
        public SeatIsNotInTheBasketException()
            : base(typeof(BLL.Exceptions.OrderExceptions.SeatIsNotInTheBasketException))
        {
        }
    }
}