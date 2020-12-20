using System.Runtime.Serialization;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class Layout
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int VenueId { get; set; }

        public TicketManagement.DAL.Models.Layout ConvertToBllLayout()
        {
            return new TicketManagement.DAL.Models.Layout()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                VenueId = this.VenueId,
            };
        }
    }
}