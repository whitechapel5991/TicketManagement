// ****************************************************************************
// <copyright file="ILayoutContract.cs" company="EPAM Systems">
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
    public interface ILayoutContract
    {
        [OperationContract]
        IEnumerable<Layout> GetLayouts();

        [OperationContract]
        Layout GetLayout(int id);

        [OperationContract]
        int AddLayout(Layout entity);

        [OperationContract]
        void UpdateLayout(Layout entity);

        [OperationContract]
        void DeleteLayout(int id);

        [OperationContract]
        IEnumerable<Layout> GetLayoutsByLayoutIds(int[] layoutIdArray);
    }

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
