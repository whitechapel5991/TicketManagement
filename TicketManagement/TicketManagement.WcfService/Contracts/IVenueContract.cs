// ****************************************************************************
// <copyright file="IVenueContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace TicketManagement.WcfService.Contracts
{
    [ServiceContract]
    public interface IVenueContract
    {
        [OperationContract]
        IEnumerable<Venue> GetVenues();

        [OperationContract]
        Venue GetVenue(int id);

        [OperationContract]
        int AddVenue(Venue entity);

        [OperationContract]
        void UpdateVenue(Venue entity);

        [OperationContract]
        void DeleteVenue(int id);
    }

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
