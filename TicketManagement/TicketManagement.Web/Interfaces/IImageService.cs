// ****************************************************************************
// <copyright file="IImageService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

namespace TicketManagement.Web.Interfaces
{
    public interface IImageService
    {
        string GetImageUri(string fileName);

        void SaveImage(string fileName, byte[] fileBytes);
    }
}
