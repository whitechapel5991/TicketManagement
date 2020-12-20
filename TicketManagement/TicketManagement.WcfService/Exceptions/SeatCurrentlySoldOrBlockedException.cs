using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class SeatCurrentlySoldOrBlockedException : WcfException
    {
        public SeatCurrentlySoldOrBlockedException()
            : base(typeof(BLL.Exceptions.OrderExceptions.SeatCurrentlySoldOrBlockedException))
        {
        }
    }
}