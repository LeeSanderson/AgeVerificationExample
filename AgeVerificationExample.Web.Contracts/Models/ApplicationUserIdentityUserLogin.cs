using Microsoft.AspNetCore.Identity;
using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    public class ApplicationUserIdentityUserLogin : IdentityUserLogin<Guid> 
    {
        /// <summary>
        /// Gets or sets the user navigation property representing the user object that the login belongs to.
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}
