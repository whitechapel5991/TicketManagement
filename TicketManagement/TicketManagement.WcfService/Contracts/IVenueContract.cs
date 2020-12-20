// ****************************************************************************
// <copyright file="IVenueContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using TicketManagement.WcfService.Exceptions.Base;

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
        [FaultContract(typeof(VenueWithThisNameExistException))]
        int AddVenue(Venue entity);

        [OperationContract]
        [FaultContract(typeof(VenueWithThisNameExistException))]
        void UpdateVenue(Venue entity);

        [OperationContract]
        void DeleteVenue(int id);
    }

    [DataContract]
    public class VenueWithThisNameExistException : WcfException
    {
        public VenueWithThisNameExistException()
            : base(typeof(BLL.Exceptions.VenueExceptions.VenueWithThisNameExistException))
        {
        }
    }
}
