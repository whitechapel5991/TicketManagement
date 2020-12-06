// ****************************************************************************
// <copyright file="IAreaContract.cs" company="EPAM Systems">
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
    public interface IAreaContract
    {
        [OperationContract]
        IEnumerable<Area> GetAreas();

        [OperationContract]
        Area GetArea(int id);

        [OperationContract]
        int AddArea(Area entity);

        [OperationContract]
        void UpdateArea(Area entity);

        [OperationContract]
        void DeleteArea(int id);
    }

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