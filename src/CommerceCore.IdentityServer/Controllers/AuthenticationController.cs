using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Client.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer.Controllers;

public class AuthenticationController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("~/callback/login/{provider}"), HttpPost("~/callback/login/{provider}"), IgnoreAntiforgeryToken]
    public async Task<ActionResult> LogInCallback()
    {
        var result = await HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

        if (result.Principal is not ClaimsPrincipal { Identity.IsAuthenticated: true })
        {
            throw new InvalidOperationException("The external authorization data cannot be used for authentication.");
        }

        // Build an identity based on the external claims and that will be used to create the authentication cookie.
        var identity = new ClaimsIdentity(
            authenticationType: "ExternalLogin",
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role);

        identity.SetClaim(ClaimTypes.Email, result.Principal.GetClaim(ClaimTypes.Email))
                .SetClaim(ClaimTypes.Name, result.Principal.GetClaim(ClaimTypes.Name))
                .SetClaim(ClaimTypes.NameIdentifier, result.Principal.GetClaim(ClaimTypes.NameIdentifier));

        // Preserve the registration details to be able to resolve them later.
        identity.SetClaim(Claims.Private.RegistrationId, result.Principal.GetClaim(Claims.Private.RegistrationId))
                .SetClaim(Claims.Private.ProviderName, result.Principal.GetClaim(Claims.Private.ProviderName));

        // Build the authentication properties based on the properties that were added when the challenge was triggered.
        var properties = new AuthenticationProperties(result.Properties.Items)
        {
            RedirectUri = result.Properties.RedirectUri
        };

        properties.StoreTokens(result.Properties.GetTokens().Where(token =>
            token.Name == OpenIddictClientAspNetCoreConstants.Tokens.RefreshToken
            || token.Name == OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken));

        return SignIn(new ClaimsPrincipal(identity), properties);
    }
}
