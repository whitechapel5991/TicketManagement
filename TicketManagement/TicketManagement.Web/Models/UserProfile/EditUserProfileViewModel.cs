using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Models.UserProfile
{
    public class EditUserProfileViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Language")]
        public Language Language { get; set; }

        [Required]
        [Display(Name = "TimeZone")]
        public string TimeZone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}