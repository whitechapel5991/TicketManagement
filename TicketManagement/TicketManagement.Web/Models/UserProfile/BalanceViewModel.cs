using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.UserProfile
{
    public class BalanceViewModel
    {
        [Required]
        [DataType(
            DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Balance", ResourceType = typeof(Resources.TicketManagementResource))]
        public decimal Balance { get; set; }
    }
}