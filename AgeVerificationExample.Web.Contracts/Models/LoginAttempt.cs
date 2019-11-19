using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    /// <summary>
    /// Model to log login attempts
    /// </summary>
    public class LoginAttempt
    {
        /// <summary>
        /// Gets or sets the unique id of the attempt
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the user that attempted to log in
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the login attempt
        /// </summary>
        public DateTime AttemptDate { get; set; }

        /// <summary>
        /// Gets or sets the status the login attempt
        /// </summary>
        public LoginAttemptStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the user that the login attempt belongs to.
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}
