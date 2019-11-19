using Microsoft.AspNetCore.Identity;
using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    public class ApplicationUserIdentityUserRole : IdentityUserRole<Guid> 
    {
        /// <summary>
        /// Gets or sets the user navigation property representing the user object.
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the user navigation property representing the role object.
        /// </summary>
        public ApplicationUserIdentityRole Role { get; set; }
    }
}
