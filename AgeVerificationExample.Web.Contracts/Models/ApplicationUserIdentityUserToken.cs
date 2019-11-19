using Microsoft.AspNetCore.Identity;
using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    public class ApplicationUserIdentityUserToken : IdentityUserToken<Guid> 
    {
        /// <summary>
        /// Gets or sets the user navigation property representing the user object.
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}
