using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SecurePassword.Config;
using System.Threading.Tasks;

namespace SecurePassword
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();

            Task.WaitAll(DbMigrationHelpers.EnsureSeedData(host));

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
