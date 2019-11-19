using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AgeVerificationExample.Web.Contracts.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// Recorded when the user registers.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// Recorded when the user registers.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>Gets or sets the login attempts associated with this user</summary>
        public virtual List<LoginAttempt> LoginAttempts { get; set; }
    }
}
