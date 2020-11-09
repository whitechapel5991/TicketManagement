// ****************************************************************************
// <copyright file="PurchaseHistoryViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;

namespace TicketManagement.Web.Models.UserProfile
{
    public class PurchaseHistoryViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
    }
}