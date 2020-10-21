// ****************************************************************************
// <copyright file="IOrderValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.ServiceValidators.Interfaces
{
    public interface IOrderValidator
    {
        void AddToCartValidation(Order newOrder);

        void BuyValidation(int orderId);

        void DeleteFromCartValidation(int orderId);
    }
}
