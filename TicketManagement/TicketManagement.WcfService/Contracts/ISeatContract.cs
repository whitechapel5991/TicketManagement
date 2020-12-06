// ****************************************************************************
// <copyright file="ISeatContract.cs" company="EPAM Systems">
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
    public interface ISeatContract
    {
        [OperationContract]
        IEnumerable<Seat> GetSeats();

        [OperationContract]
        Seat GetSeat(int id);

        [OperationContract]
        int AddSeat(Seat entity);

        [OperationContract]
        void UpdateSeat(Seat entity);

        [OperationContract]
        void DeleteSeat(int id);
    }

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
