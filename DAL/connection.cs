using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class connection
    {
        private readonly IConfiguration _configuration;
        internal connection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false);
            _configuration = builder.Build();
        }

        internal string GetConnectionString()
        {
            return _configuration.GetConnectionString("DataPath") ?? "";
        }
    }
}
