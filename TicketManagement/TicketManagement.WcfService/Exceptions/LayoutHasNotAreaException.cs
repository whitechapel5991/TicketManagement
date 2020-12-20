using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class LayoutHasNotAreaException : WcfException
    {
        public LayoutHasNotAreaException()
            : base(typeof(BLL.Exceptions.EventExceptions.LayoutHasNotAreaException))
        {
        }
    }
}