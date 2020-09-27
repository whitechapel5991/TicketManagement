using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagement.Web.Models.UserProfile
{
    public class BalanceViewModel
    {
        [Required]
        [DataType(
            DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
    }
}