using System;
using System.Runtime.Serialization;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int EventSeatId { get; set; }

        [DataMember]
        public DateTime DateUtc { get; set; }

        public TicketManagement.DAL.Models.Order ConvertToBllOrder()
        {
            return new TicketManagement.DAL.Models.Order()
            {
                Id = this.Id,
                UserId = this.UserId,
                EventSeatId = this.EventSeatId,
                DateUtc = this.DateUtc,
            };
        }
    }
}