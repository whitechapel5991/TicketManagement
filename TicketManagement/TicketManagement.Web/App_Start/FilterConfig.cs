using System.Web.Mvc;
using TicketManagement.Web.Filters;

namespace TicketManagement.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogCustomExceptionFilter());
        }
    }
}