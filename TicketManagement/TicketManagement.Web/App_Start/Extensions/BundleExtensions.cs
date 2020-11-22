// ****************************************************************************
// <copyright file="BundleExtensions.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Optimization;

namespace TicketManagement.Web.Extensions
{
    internal static class BundleExtensions
    {
        public static Bundle UnorderedBundling(this Bundle bundle)
        {
            bundle.Orderer = new UnorderedBundleOrderer();
            return bundle;
        }
    }
}