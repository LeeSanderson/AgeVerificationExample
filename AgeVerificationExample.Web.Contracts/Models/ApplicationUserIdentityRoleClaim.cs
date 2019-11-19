using Microsoft.AspNetCore.Identity;
using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    public class ApplicationUserIdentityRoleClaim : IdentityRoleClaim<Guid> 
    {
        /// <summary>
        /// Gets or sets the role navigation property.
        /// </summary>
        public ApplicationUserIdentityRole Role { get; set; }
    }

}
