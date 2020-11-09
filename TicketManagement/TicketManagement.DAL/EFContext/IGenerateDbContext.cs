// ****************************************************************************
// <copyright file="IGenerateDbContext.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.DAL.EFContext
{
    public interface IGenerateDbContext : IDisposable
    {
        TicketManagementContext GenerateNewContext();
    }
}
