using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace samsung_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => {
                    options.AddServerHeader = false;
                    options.Listen(IPAddress.Any, 5001, listenOptions =>
                    {
                        listenOptions.UseHttps("Test.crt", "Test123!");
                    });

                }) // Remove Server header,

                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .UseStartup<Startup>();
        }
    }
}