// ****************************************************************************
// <copyright file="DbCommandExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Data;

namespace TicketManagement.DAL.Extensions
{
    internal static class DbCommandExtension
    {
        public static void AddParameterWithValue<TParam>(this IDbCommand command, string paramName, TParam paramValue)
        {
            var param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;

            command.Parameters.Add(param);
        }
    }
}
