using System.ComponentModel.DataAnnotations;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required()]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required()]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required()]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required()]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required()]
        [StringLength(30, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public string TimeZone { get; set; }
    }
}