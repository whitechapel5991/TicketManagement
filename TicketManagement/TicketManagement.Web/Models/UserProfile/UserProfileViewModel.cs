using System.ComponentModel.DataAnnotations;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Models.UserProfile
{
    public class UserProfileViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

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

        [DataType(DataType.Currency)]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        public PurchaseHistoryViewModel PurchaseHistory { get; set; }
    }
}