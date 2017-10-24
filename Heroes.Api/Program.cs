using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Heroes.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .UseUrls("http://*:3000", "http://127.0.0.66:3001")
                .Build();
    }
}