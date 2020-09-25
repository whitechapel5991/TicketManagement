// ****************************************************************************
// <copyright file="TicketManagementUser.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("Users")]
    public class TicketManagementUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
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
