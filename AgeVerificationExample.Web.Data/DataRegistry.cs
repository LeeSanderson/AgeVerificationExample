using AgeVerificationExample.Web.Contracts;
using AgeVerificationExample.Web.Contracts.Repositories;
using AgeVerificationExample.Web.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AgeVerificationExample.Web.Data
{
    /// <summary>
    /// Registry for registering all database services with the service registry
    /// </summary>
    public static class DataRegistry
    {
        /// <summary>
        /// Register the repo/unit of work service with the registry
        /// </summary>
        /// <param name="registry">
        /// The registry.
        /// </param>
        public static void AddApplicationUserContext(this IServiceCollection registry)
        {
            registry.AddTransient<IApplicationUserContext, ApplicationUserContext>();
            registry.AddTransient<ILoginAttemptRepository, LoginAttemptRepository>();
            registry.AddTransient<IApplicationUserSignInManager, ApplicationUserSignInManager>();
            registry.AddTransient<IApplicationUserManager, ApplicationUserManager>();
        }
    }
}
