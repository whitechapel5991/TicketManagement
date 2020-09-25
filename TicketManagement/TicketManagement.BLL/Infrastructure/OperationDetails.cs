// ****************************************************************************
// <copyright file="OperationDetails.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

namespace TicketManagement.BLL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succedeed, string message, string prop)
        {
            this.Succedeed = succedeed;
            this.Message = message;
            this.Property = prop;
        }

        public bool Succedeed { get; private set; }

        public string Message { get; private set; }

        public string Property { get; private set; }
    }
}
