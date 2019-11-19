using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.Contracts;

[assembly: HostingStartup(typeof(AgeVerificationExample.Web.Areas.Identity.IdentityHostingStartup))]
namespace AgeVerificationExample.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            Contract.Requires(builder != null);
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}