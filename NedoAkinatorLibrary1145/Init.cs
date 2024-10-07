using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NedoAkinatorLibrary1145.DB;

namespace NedoAkinatorLibrary1145
{
    internal static class Init
    {
        public static IHost app;

        static Init()
        {
            var host = Host.CreateDefaultBuilder();
            app = host.ConfigureServices(services =>
            {
                services.AddDbContext<User01Context>();
            }).Build();
        }
    }
}
