using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AgeVerificationExample.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AgeVerificationExample.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IApplicationUserContext applicationUserContext;
        private readonly ILogger<LoginModel> logger;

        public LoginModel(IApplicationUserContext applicationUserContext, 
            ILogger<LoginModel> logger)
        {
            this.applicationUserContext = applicationUserContext;
            this.logger = logger;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {            
            if (ModelState.IsValid)
            {
                // PasswordSignInAsync first check users exists, then check password is correct and also records the login attempt
                var result = await applicationUserContext.PasswordSignInAsync(Input.Email, Input.Password);
                if (result)
                {
                    logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl ?? Url.Content("~/"));
                }
                                  
                // Username invalid or password incorrect
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }

    /// <summary>
    /// The input model for the Login page
    /// </summary>
    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
