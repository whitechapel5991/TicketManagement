using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketManagement.Web.Areas.EventManager.Data
{
    public class EventViewModel
    {
        public IndexEventViewModel IndexEventViewModel { get; set; }

        [Display(Name = "LayoutId")]
        public int LayoutId { get; set; }

        public IEnumerable<SelectListItem> LayoutList { get; set; }

    }
}