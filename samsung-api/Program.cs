using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
                .UseKestrel(c => c.AddServerHeader = false) // Remove Server header,
#if DEBUG
                .UseUrls("http://*:5002", "https://*:5003")
#else
                .UseUrls("http://*:5000", "https://*:5001")
#endif
                .UseStartup<Startup>();
        }
    }
}