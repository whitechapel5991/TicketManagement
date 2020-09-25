// ****************************************************************************
// <copyright file="UserRole.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TicketManagement.DAL.Models.Identity
{
    [Table("UserRoles")]
    public class UserRole : IdentityUserRole<int>
    {
    }
}
