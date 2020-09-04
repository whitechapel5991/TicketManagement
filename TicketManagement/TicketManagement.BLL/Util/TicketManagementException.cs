// ****************************************************************************
// <copyright file="TicketManagementException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.BLL.Util
{
    public class TicketManagementException : Exception
    {
        public TicketManagementException(string message, string prop)
            : base(message)
        {
            this.Property = prop;
            this.Source = prop;
        }

        public string Property { get; protected set; }
    }
}
