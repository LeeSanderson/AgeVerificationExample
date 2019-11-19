using System.Collections.Generic;

namespace AgeVerificationExample.Web.Contracts.Models
{
    /// <summary>
    /// Immutable result representing a success or a failure with errors
    /// </summary>
    public class SuccessOrFailureWithErrors
    {
        /// <summary>
        /// Flyweight/singleton instance of a successful action.
        /// </summary>
        public static SuccessOrFailureWithErrors Success = new SuccessOrFailureWithErrors();
        public SuccessOrFailureWithErrors(List<string> errors = null)
        {
            Errors = errors;
            Succeeded = errors == null || errors.Count == 0;
        }

        /// <summary>
        /// Gets a value indicting whether the action was successful.
        /// </summary>
        public bool Succeeded { get; }

        /// <summary>
        /// Get the errors if the action was not successful.
        /// </summary>
        public IEnumerable<string> Errors { get; }
    }
}
