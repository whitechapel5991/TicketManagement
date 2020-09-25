// ****************************************************************************
// <copyright file="TicketManagementRoleManager.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNet.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Services.Identity
{
    public class TicketManagementRoleManager : RoleManager<Role, int>
    {
        public TicketManagementRoleManager(IRoleStore<Role, int> store)
            : base(store)
        {
        }
    }
}
