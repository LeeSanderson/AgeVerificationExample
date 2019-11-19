namespace AgeVerificationExample.Web.Contracts.Models
{
    /// <summary>
    /// Status of a login attempt
    /// </summary>
    public enum LoginAttemptStatus
    {
        /// <summary>
        /// Login attempt successful
        /// </summary>
        Success = 1,

        /// <summary>
        /// Login attempt failed - probably due to an invalid password
        /// </summary>
        Fail = 2
    }
}