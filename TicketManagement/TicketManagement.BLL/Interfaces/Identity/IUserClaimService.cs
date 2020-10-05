// ****************************************************************************
// <copyright file="IUserClaimService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Security.Claims;

namespace TicketManagement.BLL.Interfaces.Identity
{
    public interface IUserClaimService
    {
        void Add(int userId, Claim claim);

        IList<Claim> GetClaims(int userId);

        void Remove(int userId, Claim claim);
    }
}
