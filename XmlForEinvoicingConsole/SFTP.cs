using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace XmlForEinvoicingConsole
{
    class SFTP
    {
        //Get server host from app.config
        public static string SFTPHost()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.JSON"));

            var root = builder.Build();
            var host = root.GetConnectionString("SFTPHost");
            return host;
        }

        //Get erver port from app.config
        public static int SFTPPort()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.JSON"));

            var root = builder.Build();
            var port = int.Parse(root.GetConnectionString("SFTPPort"));
            return port;
        }

        //Get server username from app.config
        public static string SFTPUsername()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.JSON"));

            var root = builder.Build();
            var userName = root.GetConnectionString("SFTPUsername");
            return userName;
        }

        //Get server password from app.config
        public static string SFTPPassword()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.JSON"));

            var root = builder.Build();
            var password = root.GetConnectionString("SFTPPassword");
            return password;
        }


        //Send the ZIP file which contains both the XML and PDF files to the server
        public static void UploadToSFTP(string host, int port, string username, string password, string uploadPath)
        {
            using (var client = new SftpClient(host, port, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    Debug.WriteLine("I'm connected to the client");

                    using (var fileStream = new FileStream(uploadPath, FileMode.Open))
                    {

                        client.BufferSize = 4 * 1024; //Bypass Payload error large files
                        client.UploadFile(fileStream, Path.GetFileName(uploadPath));
                    }
                }
                else
                {
                    Debug.WriteLine("I couldn't connect");
                }
                client.Disconnect();
                //client.Dispose(); Caused an error when tested, worked fine without it
            }
        }
    }
}
