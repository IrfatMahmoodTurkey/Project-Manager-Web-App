using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjectManagementWebApp.FileUploadClass
{
    public class UploadFile
    {
        
        // upload file
        public void UploadOnRemoteServer(IFormFile file, string fileName)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);

            byte[] data = ms.ToArray();

            WebClient webClient = new WebClient();

            string ftpAddress = "ftp://website/ProjectFiles/";
            string userName = "user_name";
            string password = "password";

            webClient.Credentials = new NetworkCredential(userName, password);

            webClient.UploadData(ftpAddress + fileName, data);
            webClient.Dispose();
        }

        //upload profile Picture at ftp
        public void UploadProfilePicture(IFormFile profilePicture, string fileName)
        {
            MemoryStream ms = new MemoryStream();
            profilePicture.CopyTo(ms);

            byte[] data = ms.ToArray();

            WebClient webClient = new WebClient();

            string ftpAddress = "ftp://website/ProfilePictures/";
            string userName = "user_name";
            string password = "password";

            webClient.Credentials = new NetworkCredential(userName, password);
            webClient.UploadData(ftpAddress + fileName, data);
            webClient.Dispose();
        }
    }
}
