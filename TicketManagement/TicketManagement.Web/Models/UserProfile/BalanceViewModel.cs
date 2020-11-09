// ****************************************************************************
// <copyright file="BalanceViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.UserProfile
{
    public class BalanceViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "IncreaseBalance", ResourceType = typeof(Resources.TicketManagementResource))]
        [Range(0.00, 2000000.00)]
        public decimal Balance { get; set; }
    }
}