// ****************************************************************************
// <copyright file="EnumRoleAttribute.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.Web.Constants.Extension
{
    public class EnumRoleAttribute : Attribute
    {
        public EnumRoleAttribute(string stringValue)
        {
            this.StringValue = stringValue;
        }

        public string StringValue { get; }
    }
}