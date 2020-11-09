// ****************************************************************************
// <copyright file="CartViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;

namespace TicketManagement.Web.Models.Cart
{
    public class CartViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
    }
}