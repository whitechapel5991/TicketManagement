// ****************************************************************************
// <copyright file="UserLogin.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("AspNetUserLogins")]
    public class UserLogin : UserLoginKey
    {
        [Key]
        [Column(Order = 1)]
        [Range(0, int.MaxValue)]
        public int UserId { get; set; }
    }
}
