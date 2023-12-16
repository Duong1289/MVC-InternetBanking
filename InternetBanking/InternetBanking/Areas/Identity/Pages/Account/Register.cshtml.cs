using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using InternetBanking.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using InternetBanking.Models;
using Microsoft.AspNetCore.Hosting;
using InternetBanking.Service;

namespace InternetBanking.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<InternetBankingUser> _signInManager;
        private readonly UserManager<InternetBankingUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly InternetBankingContext _internetBankingContext;
        private readonly RoleManager<IdentityRole> _roleManager; // Add this line
        private readonly IFileService _fileService;
        

        public RegisterModel(
            UserManager<InternetBankingUser> userManager,
            SignInManager<InternetBankingUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            InternetBankingContext internetBankingContext,
            RoleManager<IdentityRole> roleManager,
            IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _internetBankingContext = internetBankingContext;
            _roleManager = roleManager;
            _fileService = fileService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Required]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} numbers.", MinimumLength = 8)]
            [DataType(DataType.Text)]
            [Display(Name = "Phone Number")]
            public string Phone { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters.", MinimumLength = 12)]
            [DataType(DataType.Text)]
            [Display(Name = "Personal ID")]
            public string PersonalId { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Address")]
            public string Address { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
               
                var user = new InternetBankingUser { UserName = Input.Phone, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Customer");
                    var customer = new Customer
                    {

                        Id = user.Id,
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Phone = Input.Phone,
                        PersonalId = Input.PersonalId,
                        Address = Input.Address,
                        Status = false, // Assuming the user is not authenticated yet
                        OpenDate = DateTime.Now, // Set the account open date
                                                 // Set other properties as needed
                    };

                    _internetBankingContext.Customers.Add(customer);
                    await _internetBankingContext.SaveChangesAsync();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Verify Your Email",
                $"<!DOCTYPE html>" +
                $"<html lang=\"en\">" +
                $"<head>" +
                $"<meta charset=\"UTF-8\">" +
                $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                $"<style>" +
                $"   .button {{ display: inline-block; padding: 10px 20px; font-size: 16px; text-align: center; text-decoration: none; background-color: #007bff; color: #ffffff; border-radius: 5px; }}" +
                $"</style>" +
                $"</head>" +
                $"<body>" +
                $"   <p>Please confirm your account by clicking the button below:</p>" +
                $"   <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='button'>Confirm Your Email</a>" +
                $"</body>" +
                $"</html>");



                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

       
    }
}
