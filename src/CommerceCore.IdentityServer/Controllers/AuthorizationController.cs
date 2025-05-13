using System.Security.Claims;
using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer.Controllers;

public class AuthorizationController(
    IOpenIddictApplicationManager applicationManager,
    IOpenIddictAuthorizationManager authorizationManager,
    UserManager<ApplicationUser> userManager
) : Controller
{
    private readonly IOpenIddictApplicationManager _applicationManager = applicationManager;
    private readonly IOpenIddictAuthorizationManager _authorizationManager = authorizationManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        var request =
            HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException(
                "The OpenID Connect request cannot be retrieved."
            );

        var result = await HttpContext.AuthenticateAsync();

        if (!result.Succeeded)
            return Challenge(
                new AuthenticationProperties
                {
                    RedirectUri =
                        Request.PathBase
                        + Request.Path
                        + QueryString.Create(
                            Request.HasFormContentType ? Request.Form : Request.Query
                        )
                }
            );

        var user =
            await _userManager.GetUserAsync(result.Principal)
            ?? throw new InvalidOperationException("The user details cannot be retrieved.");

        var application =
            await _applicationManager.FindByClientIdAsync(request.ClientId)
            ?? throw new InvalidOperationException("The application details cannot be retrieved.");

        var authorizations = _authorizationManager
            .FindAsync(
                user.Id,
                request.ClientId,
                Statuses.Valid,
                AuthorizationTypes.AdHoc,
                request.GetScopes()
            )
            .ToBlockingEnumerable()
            .ToList();

        var identity = new ClaimsIdentity(
            TokenValidationParameters.DefaultAuthenticationType,
            Claims.Name,
            Claims.Role
        );

        identity
            .SetClaim(Claims.Subject, user.Id)
            .SetClaim(Claims.Email, user.Email)
            .SetClaim(Claims.Name, user.UserName)
            .SetClaims(Claims.Role, [.. await _userManager.GetRolesAsync(user)]);

        identity.SetScopes(request.GetScopes());

        var authorization = authorizations.LastOrDefault();
        authorization ??= await _authorizationManager.CreateAsync(
            identity,
            user.Id,
            request.ClientId,
            AuthorizationTypes.AdHoc,
            identity.GetScopes()
        );

        identity.SetAuthorizationId(await _authorizationManager.GetIdAsync(authorization));
        identity.SetDestinations(GetDestinations);

        return SignIn(
            new ClaimsPrincipal(identity),
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
        );
    }

    [HttpPost("~/connect/token")]
    [Produces("application/json")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Exchange()
    {
        var request =
            HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException(
                "The OpenID Connect request cannot be retrieved."
            );

        if (!request.IsAuthorizationCodeGrantType() && !request.IsRefreshTokenGrantType())
            throw new InvalidOperationException("The specified grant type is not supported.");

        var result = await HttpContext.AuthenticateAsync(
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
        );

        var userId = result?.Principal?.GetClaim(Claims.Subject);

        if (userId is null) return Forbid();

        var user = await _userManager.FindByIdAsync(userId);

        var identity = new ClaimsIdentity(
            result.Principal.Claims,
            TokenValidationParameters.DefaultAuthenticationType,
            Claims.Name,
            Claims.Role
        );

        // Override the user claims present in the principal
        identity
            .SetClaim(Claims.Subject, user.Id)
            .SetClaim(Claims.Email, user.Email)
            .SetClaim(Claims.Name, user.UserName)
            .SetClaims(Claims.Role, [.. await _userManager.GetRolesAsync(user)]);

        identity.SetDestinations(GetDestinations);

        return SignIn(
            new ClaimsPrincipal(identity),
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
        );
    }

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        yield return Destinations.AccessToken;

        switch (claim.Type)
        {
            case Claims.Name:
                if (claim.Subject.HasScope(Scopes.Profile))
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Email:
                if (claim.Subject.HasScope(Scopes.Email))
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Role:
                if (claim.Subject.HasScope(Scopes.Roles))
                    yield return Destinations.IdentityToken;

                yield break;

            default:
                yield break;
        }
    }

    [HttpGet("~/connect/userinfo")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> GetUserInfo()
    {
        var result = await HttpContext.AuthenticateAsync(
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
        );

        var userId = result?.Principal?.GetClaim(Claims.Subject);

        if (userId is null) return Forbid();

        var user = await _userManager.FindByIdAsync(userId);

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        return Ok(
            new UserViewModel
            {
                Id = user?.Id,
                UserName = user?.UserName,
                Email = user?.Email,
                PhoneNumber = user?.PhoneNumber,
                Role = role
            }
        );
    }

    [HttpGet("~/connect/logout")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("access_token");
        Response.Cookies.Delete("refresh_token");

        if (Request.Query.ContainsKey("post_logout_redirect_uri") == false) return Ok();

        return Redirect(Request.Query["post_logout_redirect_uri"].ToString());
    }
}