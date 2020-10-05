// ****************************************************************************
// <copyright file="TicketManagementUser.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("AspNetUsers")]
    public class TicketManagementUser : Entity
    {
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEndDateUtc { get; set; }

        public int AccessFailedCount { get; set; }

        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("TimeZone")]
        public string TimeZone { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("Language")]
        public string Language { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Surname")]
        public string Surname { get; set; }

        [Required]
        [Column("Balance")]
        public decimal Balance { get; set; }

        [NotMapped]
        public string Password { get; set; }
    }
}
