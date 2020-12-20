using System.Runtime.Serialization;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class Venue
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Phone { get; set; }

        public TicketManagement.DAL.Models.Venue ConvertToBllVenue()
        {
            return new TicketManagement.DAL.Models.Venue()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Address = this.Address,
                Phone = this.Phone,
            };
        }
    }
}