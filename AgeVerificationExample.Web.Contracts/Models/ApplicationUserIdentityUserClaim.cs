using Microsoft.AspNetCore.Identity;
using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    public class ApplicationUserIdentityUserClaim : IdentityUserClaim<Guid> 
    {
        /// <summary>
        /// Gets or sets the navigation property that represents the user that this claim is associated with.
        /// </summary>
        public ApplicationUser User { get; set; }
    }

}
