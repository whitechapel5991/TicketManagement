using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Account
{
    public class UserPasswordViewModel
    {
        [Required()]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }

        [Required()]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm")]
        public string PasswordConfirm { get; set; }
    }
}