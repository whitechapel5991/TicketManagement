using System.Runtime.Serialization;
using TicketManagement.DAL.Constants;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class EventSeat
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public EventSeatState State { get; set; }

        [DataMember]
        public int EventAreaId { get; set; }

        public TicketManagement.DAL.Models.EventSeat ConvertToBllEventSeat()
        {
            return new TicketManagement.DAL.Models.EventSeat()
            {
                Id = this.Id,
                Row = this.Row,
                Number = this.Number,
                State = this.State,
                EventAreaId = this.EventAreaId,
            };
        }
    }
}