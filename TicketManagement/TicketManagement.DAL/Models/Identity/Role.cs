// ****************************************************************************
// <copyright file="Role.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("AspNetRoles")]
    public class Role : Entity
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
