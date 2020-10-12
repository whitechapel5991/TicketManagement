// ****************************************************************************
// <copyright file="ClaimBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models.Identity
{
    public abstract class ClaimBase : Entity
    {
        [StringLength(int.MaxValue)]
        public string ClaimType { get; set; }

        [StringLength(int.MaxValue)]
        public string ClaimValue { get; set; }
    }
}
