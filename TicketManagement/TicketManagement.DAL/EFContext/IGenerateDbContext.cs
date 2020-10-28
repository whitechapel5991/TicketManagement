using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DAL.EFContext
{
    public interface IGenerateDbContext : IDisposable
    {
        TicketManagementContext GenerateNewContext();
    }
}
