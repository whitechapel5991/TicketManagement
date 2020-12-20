using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Exceptions
{
    [DataContract]
    public class AreaWithSameDescriptionInTheLayoutExistException : WcfException
    {
        public AreaWithSameDescriptionInTheLayoutExistException()
            : base(typeof(BLL.Exceptions.AreaExceptions.AreaWithSameDescriptionInTheLayoutExistException))
        {
        }
    }
}