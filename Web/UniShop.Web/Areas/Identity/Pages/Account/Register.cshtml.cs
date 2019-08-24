using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using UniShop.Data.Models;

namespace UniShop.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UniShopUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UniShopUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
       // private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<UniShopUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<UniShopUser> signInManager,
            ILogger<RegisterModel> logger
          //  IEmailSender emailSender
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
           // _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
            [Display(Name = "Потребителско име")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
            [Display(Name = "Имена")]
            public string FullName { get; set; }

            
            [EmailAddress(ErrorMessage ="Невалиден имейл.")]
            [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
            [Display(Name = "Емайл")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
            //[DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            // [DataType(DataType.Password)]
            [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
            [Display(Name = "Потвърди паролата")]
            [Compare("Password", ErrorMessage = "Паролата не съвпада")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = "/Identity/Account/Login";

            if (ModelState.IsValid)
            {
                var isRoot = !_userManager.Users.Any();
                var user = new UniShopUser {
                    UserName = Input.UserName,
                    Email = Input.Email ,
                    FullName =Input.FullName ,
                    ShoppingCart = new ShoppingCart()
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (isRoot)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    #region Email Func
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    #endregion
                    return LocalRedirect(returnUrl);
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
