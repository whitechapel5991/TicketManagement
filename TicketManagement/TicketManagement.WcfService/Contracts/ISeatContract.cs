// ****************************************************************************
// <copyright file="ISeatContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ServiceModel;
using EntityDoesNotExistException = TicketManagement.WcfService.Exceptions.EntityDoesNotExistException;

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
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(SeatWithSameRowAndNumberInTheAreaExistException))]
        int AddSeat(Seat entity);

        [OperationContract]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(SeatWithSameRowAndNumberInTheAreaExistException))]
        void UpdateSeat(Seat entity);

        [OperationContract]
        void DeleteSeat(int id);
    }
}
