using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Event")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "BeginDate")]
        public DateTime BeginDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
        [Display(Name = "BeginTime")]
        public DateTime BeginTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
        [Display(Name = "EndTime")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(
            300,
            MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "CountFreeSeats")]
        public int CountFreeSeats { get; set; }

        [Display(Name = "Layout")]
        public string LayoutName { get; set; }
    }
}