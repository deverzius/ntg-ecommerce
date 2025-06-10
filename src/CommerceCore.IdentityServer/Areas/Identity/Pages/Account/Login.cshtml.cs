// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceCore.IdentityServer.Areas.Identity.Pages.Account;

public class LoginModel(
    SignInManager<AppUser> signInManager,
    ILogger<LoginModel> logger
) : PageModel
{
    private readonly ILogger<LoginModel> _logger = logger;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [BindProperty] public LoginInput Input { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public string ReturnUrl { get; set; }

    [TempData] public string ErrorMessage { get; set; }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage)) ModelState.AddModelError(string.Empty, ErrorMessage);

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ExternalLogins = (
            await _signInManager.GetExternalAuthenticationSchemesAsync()
        ).ToList();

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        ExternalLogins = (
            await _signInManager.GetExternalAuthenticationSchemesAsync()
        ).ToList();

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                Input.Email,
                Input.Password,
                Input.RememberMe,
                false
            );
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        return Page();
    }
}