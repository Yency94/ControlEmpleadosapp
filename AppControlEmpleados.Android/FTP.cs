using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppControlEmpleados.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FTP))]
namespace AppControlEmpleados.Droid
{
    class FTP: IFtpWebRequest
    {
        public FTP()
        {

        }

        public string upload(string FtpUrl, string fileName, string userName, string password, string UploadDirectory = "")
        {
            try
            {

                string PureFileName = new FileInfo(fileName).Name;
                String uploadUrl = String.Format("{0}{1}/{2}", FtpUrl, UploadDirectory, PureFileName);
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(uploadUrl);
                req.Proxy = null;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(userName, password);
                req.UseBinary = true;
                req.UsePassive = true;
                byte[] data = File.ReadAllBytes(fileName);
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                FtpWebResponse res = (FtpWebResponse)req.GetResponse();
                return res.StatusDescription;

            }
            catch (Exception err)
            {
                return err.ToString();
            }
        }
    }
}