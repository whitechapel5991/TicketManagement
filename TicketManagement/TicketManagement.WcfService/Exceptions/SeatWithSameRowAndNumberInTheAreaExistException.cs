using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class SeatWithSameRowAndNumberInTheAreaExistException : WcfException
    {
        public SeatWithSameRowAndNumberInTheAreaExistException()
            : base(typeof(BLL.Exceptions.SeatExceptions.SeatWithSameRowAndNumberInTheAreaExistException))
        {
        }
    }
}