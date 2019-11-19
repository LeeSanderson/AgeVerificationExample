namespace AgeVerificationExample.Web.Contracts.Models
{
    /// <summary>
    /// Model for counts of login attempts grouped by status
    /// </summary>
    public class LoginStatusCount
    {
        /// <summary>
        /// Gets or sets the status the login attempt
        /// </summary>
        public LoginAttemptStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the count of the number of attempts for the given status.
        /// </summary>
        public int Count { get; set; }
    }
}
