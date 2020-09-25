// ****************************************************************************
// <copyright file="TicketManagementUserManager.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNet.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Services.Identity
{
    public class TicketManagementUserManager : UserManager<TicketManagementUser, int>
    {
        public TicketManagementUserManager(IUserStore<TicketManagementUser, int> store)
            : base(store)
        {
        }
    }
}
