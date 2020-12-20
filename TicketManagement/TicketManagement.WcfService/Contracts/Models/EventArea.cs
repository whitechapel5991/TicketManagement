using System.Runtime.Serialization;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class EventArea
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int CoordinateX { get; set; }

        [DataMember]
        public int CoordinateY { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int EventId { get; set; }

        public TicketManagement.DAL.Models.EventArea ConvertToBllEventArea()
        {
            return new TicketManagement.DAL.Models.EventArea()
            {
                Id = this.Id,
                Description = this.Description,
                CoordinateX = this.CoordinateX,
                CoordinateY = this.CoordinateY,
                Price = this.Price,
                EventId = this.EventId,
            };
        }
    }
}