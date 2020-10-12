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
        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        [MaxLength(int.MaxValue)]
        public string PasswordHash { get; set; }

        [MaxLength(int.MaxValue)]
        public string SecurityStamp { get; set; }

        [MaxLength(int.MaxValue)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public bool PhoneNumberConfirmed { get; set; }

        [Required]
        public bool TwoFactorEnabled { get; set; }

        [Required]
        public bool LockoutEnabled { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? LockoutEndDateUtc { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AccessFailedCount { get; set; }

        [MaxLength(256)]
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
