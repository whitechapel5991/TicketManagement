using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class BeginDateLongerThenEndDateException : WcfException
    {
        public BeginDateLongerThenEndDateException()
            : base(typeof(BLL.Exceptions.EventExceptions.BeginDateLongerThenEndDateException))
        {
        }
    }
}