// ****************************************************************************
// <copyright file="DbCommandExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DAL.Extensions
{
    public static class DbCommandExtension
    {
        public static void AddParameterWithValue(this DbCommand command, string paramName, object paramValue)
        {
            var param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;

            command.Parameters.Add(param);
        }
    }
}
