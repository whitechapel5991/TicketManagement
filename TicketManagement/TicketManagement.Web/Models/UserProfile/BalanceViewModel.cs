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
        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "FieldIsRequired")]
        [DataType(
            DataType.Currency,
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "IncorrectCurrency")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "IncreaseBalance", ResourceType = typeof(Resources.TicketManagementResource))]
        [Range(0.00, 1000000.00, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "BalanceRangeError")]
        public decimal Balance { get; set; }
    }
}