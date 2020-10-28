using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
