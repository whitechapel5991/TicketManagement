using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.Web.Interfaces
{
    public interface IImageService
    {
        string GetImageUri(string fileName);

        void SaveImage(string fileName, byte[] fileBytes);
    }
}
