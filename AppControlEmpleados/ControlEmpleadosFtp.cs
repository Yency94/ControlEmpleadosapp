using System;
using System.Collections.Generic;
using System.Text;

namespace AppControlEmpleados
{
    public interface IFtpWebRequest
    {
        string upload(string FtpUrl, string fileName, string userName, string password, string UploadDirectory = "");
    }
}
