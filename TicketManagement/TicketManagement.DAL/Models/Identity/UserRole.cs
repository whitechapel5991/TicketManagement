// ****************************************************************************
// <copyright file="UserRole.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("AspNetUserRoles")]
    public class UserRole
    {
        [Required]
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [Required]
        [Key]
        [Column(Order = 2)]
        public int RoleId { get; set; }
    }
}
