// ****************************************************************************
// <copyright file="UserLoginKey.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DAL.Models.Identity
{
    public class UserLoginKey
    {
        [Key]
        [Required]
        [Column(Order = 2)]
        [MaxLength(128)]
        public string LoginProvider { get; set; }

        [Key]
        [Required]
        [Column(Order = 3)]
        [MaxLength(128)]
        public string ProviderKey { get; set; }
    }
}
