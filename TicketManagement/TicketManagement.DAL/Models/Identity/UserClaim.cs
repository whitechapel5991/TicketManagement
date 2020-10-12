// ****************************************************************************
// <copyright file="UserClaim.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("AspNetUserClaims")]

    public class UserClaim : ClaimBase
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int UserId { get; set; }
    }
}
