using System.Runtime.Serialization;
using TicketManagement.WcfService.Exceptions.Base;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class LayoutWithSameNameInTheVenueExistException : WcfException
    {
        public LayoutWithSameNameInTheVenueExistException()
            : base(typeof(BLL.Exceptions.LayoutExceptions.LayoutWithSameNameInTheVenueExistException))
        {
        }
    }
}