// ****************************************************************************
// <copyright file="ILayoutContract.cs" company="EPAM Systems">
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
    public interface ILayoutContract
    {
        [OperationContract]
        IEnumerable<Layout> GetLayouts();

        [OperationContract]
        Layout GetLayout(int id);

        [OperationContract]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(LayoutWithSameNameInTheVenueExistException))]
        int AddLayout(Layout entity);

        [OperationContract]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(LayoutWithSameNameInTheVenueExistException))]
        void UpdateLayout(Layout entity);

        [OperationContract]
        void DeleteLayout(int id);

        [OperationContract]
        IEnumerable<Layout> GetLayoutsByLayoutIds(int[] layoutIdArray);
    }
}
