using System;

namespace AgeVerificationExample.Web.Contracts.Models
{
    /// <summary>
    /// Model to represent a attempted registration attempt.
    /// </summary>
    public class RegistrationAttempt
    {
        /// <summary>
        /// Gets or sets the unique id of the attempt
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name used during registration
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address used during registration
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last registration attempt
        /// using the same combination of name and email
        /// </summary>
        public DateTime LastAttempt { get; set; }

        /// <summary>
        /// Gets or sets the number of times that these credentials have failed
        /// to pass the registration process due to the used being under age.
        /// </summary>
        public int Failures { get; set; }

        /// <summary>
        /// Gets or sets the date and time that the registration attempt was locked out.
        /// </summary>
        public DateTime? LockedOutDate { get; set; }
    }
}
