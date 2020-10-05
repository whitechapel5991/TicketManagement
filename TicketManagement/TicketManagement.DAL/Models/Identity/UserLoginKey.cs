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

    public class UserLoginKey
    {
        [Key]
        [Column(Order = 2)]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 3)]
        public string ProviderKey { get; set; }
    }
}
