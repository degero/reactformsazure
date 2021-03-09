using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // Dump env vars
            // var config = host.Services.GetRequiredService<IConfiguration>();
            // var env = host.Services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();

            // Console.WriteLine("is dev: " + env.IsDevelopment());
            // foreach (var c in config.AsEnumerable())
            // {
            //     Console.WriteLine(c.Key + " = " + c.Value);
            // }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        // Custom env var override
        //         .ConfigureAppConfiguration((hostingContext, config) =>
        //     {
        //     config.AddEnvironmentVariables(prefix: "myapp_");
        // });
    }
}
