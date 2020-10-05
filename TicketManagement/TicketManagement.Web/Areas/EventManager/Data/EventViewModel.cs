using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TicketManagement.Web.Areas.EventManager.Data
{
    public class EventViewModel
    {
        public IndexEventViewModel IndexEventViewModel { get; set; }

        [Display(Name = "LayoutId", ResourceType = typeof(Resources.TicketManagementResource))]
        public int LayoutId { get; set; }

        public IEnumerable<SelectListItem> LayoutList { get; set; }

    }
}