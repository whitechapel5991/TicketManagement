// ****************************************************************************
// <copyright file="SeatWithSameRowAndNumberInTheAreaExistException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.SeatExceptions
{
    public class SeatWithSameRowAndNumberInTheAreaExistException : Exception
    {
        public SeatWithSameRowAndNumberInTheAreaExistException(string message)
            : base(message)
        {
        }
    }
}
