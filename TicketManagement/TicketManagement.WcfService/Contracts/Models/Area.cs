using System.Runtime.Serialization;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class Area
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
        public int LayoutId { get; set; }

        public TicketManagement.DAL.Models.Area ConvertToBllArea()
        {
            return new TicketManagement.DAL.Models.Area()
            {
                Id = this.Id,
                Description = this.Description,
                CoordinateX = this.CoordinateX,
                CoordinateY = this.CoordinateY,
                LayoutId = this.LayoutId,
            };
        }
    }
}