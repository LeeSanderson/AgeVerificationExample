using AgeVerificationExample.Web.Contracts;
using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AgeVerificationExample.Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// After validtion on registation page is successful, allow the user to create a password
    /// </summary>
    public class CreatePasswordModel : PageModel
    {
        private readonly IApplicationUserContext applicationUserContext;
        private readonly ILogger<CreatePasswordModel> logger;

        public CreatePasswordModel(
            IApplicationUserContext applicationUserContext,
            ILogger<CreatePasswordModel> logger)
        {
            this.applicationUserContext = applicationUserContext;
            this.logger = logger;
        }
        
        [BindProperty]
        public PasswordInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            var registrationModel = TempData.Get<RegisterInputModel>("RegisterInputModel");
            if (registrationModel == null)
            {
                ModelState.AddModelError(string.Empty, "Please complete the registration form in order to continue");
                return RedirectToPage("Register", new { ReturnUrl });
            }

            // Keep tempdata so we can process the registration model on postback
            TempData.Keep();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var registrationModel = TempData.Get<RegisterInputModel>("RegisterInputModel");
                if (registrationModel == null)
                {
                    ModelState.AddModelError(string.Empty, "Please complete the registration form in order to continue");
                    return RedirectToPage("Register", new { ReturnUrl });
                }

                var user = 
                   new ApplicationUser 
                        { 
                            Id = Guid.NewGuid(),
                            Name = registrationModel.Name,
                            UserName = registrationModel.Email, 
                            Email = registrationModel.Email, 
                            DateOfBirth = registrationModel.DateOfBirth.Value 
                        };
                var result = await applicationUserContext.CreateUserAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation($"User created a new account with email {registrationModel.Email}.");

                    // TODO: Add alert message? "Thanks for registering. Please login"
                    return RedirectToPage("Login",new { returnUrl });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            // If we got this far, something failed, redisplay form
            TempData.Keep();
            return Page();
        }
    }

    public class PasswordInputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long and contain one number, one lowercase character, one uppercase character and one symbol.", MinimumLength = 12)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
