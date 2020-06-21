using DLL.BLL.Models;
using DLL.DAL.Repository.Contracts;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStore.Unit_Of_Work;

namespace WinStore.Configuracao
{

    public static class Config
    {
        public static IConfiguration configuration;

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        public static string Get(string name)
        {
            string appSettings = configuration[name];
            return appSettings;
        }
    }
}
