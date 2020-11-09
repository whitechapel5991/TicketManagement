// ****************************************************************************
// <copyright file="GenerateDbContext.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

namespace TicketManagement.DAL.EFContext
{
    internal class GenerateDbContext : IGenerateDbContext
    {
        private readonly string connectionString;
        private TicketManagementContext context;

        public GenerateDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }

        public TicketManagementContext GenerateNewContext()
        {
            if (this.context != null)
            {
                return this.context;
            }

            this.context = new TicketManagementContext(this.connectionString);
            return this.context;
        }
    }
}
