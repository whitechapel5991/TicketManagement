using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Exceptions
{
    [DataContract]
    public class EntityDoesNotExistException : WcfException
    {
        public EntityDoesNotExistException()
            : base(typeof(BLL.Exceptions.Base.EntityDoesNotExistException))
        {
        }
    }
}