// ****************************************************************************
// <copyright file="EventImageService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.IO;
using TicketManagement.Web.Interfaces;

namespace TicketManagement.Web.Services
{
    public class EventImageService : IImageService
    {
        private readonly string pathBase;

        public EventImageService(string pathBase)
        {
            this.pathBase = Path.Combine(pathBase);
        }

        public string GetImageUri(string fileName)
        {
            return Path.Combine(this.pathBase, fileName);
        }

        public void SaveImage(string fileName, byte[] fileBytes)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            if (!Directory.Exists(this.pathBase))
            {
                var fullFolderPath = this.pathBase;
                var folderPath = new Uri(fullFolderPath).LocalPath;
                Directory.CreateDirectory(folderPath);
            }

            var fullFilePath = this.GetImageUri(fileName);
            var fs = File.Create(fullFilePath);
            fs.Flush();
            fs.Dispose();
            fs.Close();

            var sw = new BinaryWriter(new FileStream(fullFilePath, FileMode.Create, FileAccess.Write));
            sw.Write(fileBytes);

            sw.Flush();
            sw.Dispose();
            sw.Close();
        }
    }
}