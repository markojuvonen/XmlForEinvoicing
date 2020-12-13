using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XmlForEinvoicingLib
{
    public static class DbConnection
    {
        //Get the database connection string from AppSettings.JSON
        public static string ConnString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.JSON"));

            var root = builder.Build();
            var connString = root.GetConnectionString("MyyntiDB");
            return connString;
        }       
    }
}
