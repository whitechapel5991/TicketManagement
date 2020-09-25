using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required()]
        [Display(Name = "EventName")]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required()]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}