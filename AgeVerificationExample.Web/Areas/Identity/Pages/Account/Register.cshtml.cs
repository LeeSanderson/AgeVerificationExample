using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AgeVerificationExample.Web.Contracts;
using AgeVerificationExample.Web.Contracts.Models;
using AgeVerificationExample.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AgeVerificationExample.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IApplicationUserContext applicationUserContext;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(
            IApplicationUserContext applicationUserContext,
            ILogger<RegisterModel> logger)
        {
            this.applicationUserContext = applicationUserContext;
            this.logger = logger;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var registrationAttemptRepository = applicationUserContext.RegistrationAttemptRepository;
                var registrationAttempt = await registrationAttemptRepository.GetByEmailAndNameAsync(Input.Email, Input.Name);
                if (registrationAttempt == null)
                {
                    registrationAttempt = new RegistrationAttempt { Id = Guid.NewGuid(), Email = Input.Email, Name = Input.Name };
                    registrationAttemptRepository.Add(registrationAttempt);
                }

                if (registrationAttempt.LockedOutDate != null)
                {
                    // Registrant is locked out. Always display error message for this user
                    logger.LogInformation($"User {Input.Name} with email {Input.Email} is locked out.");
                    AddLockedOutErrorMessage();
                    return Page();
                }

                // Calculate age of registrant
                var dob = Input.DateOfBirth.Value;
                var today = DateTime.Today;
                var yearOfBirth = dob.Year;
                var age = (today.Year - yearOfBirth);
                if (today.Month < dob.Month || today.Month == dob.Month && today.Day <= dob.Day)
                {
                    age++;
                }

                // Validate age of registrant.
                registrationAttempt.LastAttempt = DateTime.UtcNow;
                if (age < 18)
                {
                    if ((DateTime.UtcNow - registrationAttempt.LastAttempt).TotalMinutes > 60)
                    {
                        // Last failure was more than an hour ago - reset failure counter
                        registrationAttempt.Failures = 1;
                    }
                    else
                    {
                        // Last failure was within an hour - increase failures and check for lockout 
                        registrationAttempt.Failures++;
                        if (registrationAttempt.Failures >= 3)
                        {
                            registrationAttempt.LockedOutDate = DateTime.UtcNow;
                        }
                    }

                    ModelState.AddModelError(string.Empty, "You must be at least 18 to register");
                    Input.DateOfBirth = null;
                }

                // Save changes to registration attempts in database
                await this.applicationUserContext.SaveChangesAsync();

                // Check to see if we have just locked out user due to 3rd failure attempt.
                if (registrationAttempt.LockedOutDate != null)
                {
                    // Registrant has just been locked out. Display error message for this user
                    logger.LogInformation($"User {Input.Name} with email {Input.Email} is locked out.");
                    AddLockedOutErrorMessage();
                    return Page();
                }

                if (age >= 18)
                {
                    // Registration details are valid - save details in temp data and redirect to page to create password
                    TempData.Set("RegisterInputModel", Input);
                    return RedirectToPage("CreatePassword", new { ReturnUrl });
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private void AddLockedOutErrorMessage()
        {
            ModelState.AddModelError(string.Empty, "Your registration details have been locked out due to multiple registration failures.");
        }
    }

    public class RegisterInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
    }
}
