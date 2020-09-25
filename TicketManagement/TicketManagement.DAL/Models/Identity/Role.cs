// ****************************************************************************
// <copyright file="Role.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("Roles")]
    public class Role : IdentityRole<int, UserRole>
    {
        public Role()
        {
        }

        public Role(string name)
        {
            this.Name = name;
        }
    }
}
