// ****************************************************************************
// <copyright file="ClaimBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models.Identity
{
    public abstract class ClaimBase : Entity
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
