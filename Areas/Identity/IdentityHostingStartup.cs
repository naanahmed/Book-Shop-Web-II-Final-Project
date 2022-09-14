using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Book_Shop.Areas.Identity.IdentityHostingStartup))]
namespace Book_Shop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}