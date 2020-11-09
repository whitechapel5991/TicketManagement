// ****************************************************************************
// <copyright file="ICartService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.Web.Models.Cart;

namespace TicketManagement.Web.Interfaces
{
    public interface ICartService
    {
        CartViewModel GetCartViewModelByUserName(string userName);

        void Buy(int orderId);

        void Delete(int orderId);
    }
}
