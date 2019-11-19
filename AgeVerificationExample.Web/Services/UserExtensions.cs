using System;
using System.Security.Claims;
using System.Security.Principal;

namespace AgeVerificationExample.Web.Services
{
    /// <summary>
    /// Extension methods for user and principle objects that are part of System.Security and used by
    /// identity framework.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Get the ID (from the ApplicationUsers table for the currertly signed in user)
        /// Typically used as follows in both views and controllers: <code>User.GetUserId()</code>
        /// Obviously this method only works when a user is logged in
        /// </summary>
        /// <param name="principal">The principle of the current user</param>
        /// <returns>The GUID representing the users unique id</returns>
        public static Guid GetUserId(this IPrincipal principal)
        {
            var claim = GetUserClaim(principal, ClaimTypes.NameIdentifier);

            Guid userId;
            if (!Guid.TryParse(claim.Value, out userId))
            {
                throw new Exception($"Invalid principle/identity. Value of ClaimTypes.NameIdentifier is '{claim.Value}' which is not a valid Guid");
            }

            return userId;
        }

        /// <summary>
        /// Gets a given user claim form the <see cref="principal"/>.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="claimType">The claim type to return .</param>
        /// <returns>
        /// The <see cref="Claim"/>.
        /// </returns>
        private static Claim GetUserClaim(IPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claimsIdentity = principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                throw new Exception("Invalid principle/identity. Please ensure User.Identity is set to valid ClaimsIdentity");
            }

            if (!claimsIdentity.IsAuthenticated)
            {
                throw new Exception(
                    "Invalid principle/identity. ClaimsIdentity must be authenticated in order to invoke GetUserId()");
            }

            var claim = claimsIdentity.FindFirst(claimType);
            if (claim == null)
            {
                throw new Exception(
                    $"Invalid principle/identity. Please ensure ClaimsIdentity is set up with a valid {claimType}");
            }

            return claim;
        }
    }
}
