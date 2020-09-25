using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketManagement.Web.Areas.EventManager.Data
{
    public class IndexEventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "EventName")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "BeginDate")]
        public DateTime BeginDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "BeginTime")]
        public DateTime BeginTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "EndTime")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(
            300,
            MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "LayoutId")]
        public int LayoutId { get; set; }

        [Display(Name = "Layout")]
        public string LayoutName { get; set; }

        [Display(Name = "Published")]
        public bool Published { get; set; }
    }
}