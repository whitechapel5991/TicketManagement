using System.Runtime.Serialization;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class Seat
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public int AreaId { get; set; }

        public TicketManagement.DAL.Models.Seat ConvertToBllSeat()
        {
            return new TicketManagement.DAL.Models.Seat()
            {
                Id = this.Id,
                Row = this.Row,
                Number = this.Number,
                AreaId = this.AreaId,
            };
        }
    }
}