// ****************************************************************************
// <copyright file="IAreaContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ServiceModel;
using TicketManagement.WcfService.Exceptions;

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
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(AreaWithSameDescriptionInTheLayoutExistException))]
        int AddArea(Area entity);

        [OperationContract]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(AreaWithSameDescriptionInTheLayoutExistException))]
        void UpdateArea(Area entity);

        [OperationContract]
        void DeleteArea(int id);
    }
}