using System.Web.Mvc;
using TicketManagement.Web.Filters;

namespace TicketManagement.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogCustomExceptionFilter(), order: int.MaxValue);
            filters.Add(new RedirectExceptionFilter(), order: int.MinValue);
        }
    }
}