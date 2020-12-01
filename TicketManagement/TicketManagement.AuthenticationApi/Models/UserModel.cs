using System.ComponentModel.DataAnnotations;

namespace TicketManagement.AuthenticationApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "UserName must has symbols from 3 to 30")]
        public string UserName { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email.")]
        public string Email { get; set; }

        [Required]
        public string TimeZone { get; set; }

        [Required]
        public string Language { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "FirstName must has symbols from 2 to 30")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "SurName is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Surname must has symbols from 2 to 30")]
        public string Surname { get; set; }

        public decimal Balance { get; set; }
    }
}
