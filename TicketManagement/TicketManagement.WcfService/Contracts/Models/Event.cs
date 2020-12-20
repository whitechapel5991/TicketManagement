using System;
using System.Runtime.Serialization;

namespace TicketManagement.WcfService.Contracts
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime BeginDateUtc { get; set; }

        [DataMember]
        public DateTime EndDateUtc { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public int LayoutId { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        public TicketManagement.DAL.Models.Event ConvertToBllEvent()
        {
            return new TicketManagement.DAL.Models.Event()
            {
                Id = this.Id,
                Name = this.Name,
                BeginDateUtc = this.BeginDateUtc,
                EndDateUtc = this.EndDateUtc,
                Description = this.Description,
                Published = this.Published,
                LayoutId = this.LayoutId,
                ImageUrl = this.ImageUrl,
            };
        }
    }
}