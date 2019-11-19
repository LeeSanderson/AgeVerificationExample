using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    /// <summary>
    /// A paged list of <see cref="LoginAttempt"/> instances to be used when displaying a list of login attempts for a given user.
    /// </summary>
    public class LoginAttemptPagedList : PagedList<LoginAttempt>
    {
        /// <summary>
        /// Gets or sets the id of the user that we want to get the login attempts list for.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
