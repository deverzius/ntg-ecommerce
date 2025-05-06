using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.Controllers;

[Route("[controller]/[action]")]
public class AuthController(
    ILogger<AuthController> logger,
    HttpClient httpClient,
    IConfiguration config
) : Controller
{
    private readonly string _clientUrl =
        config["Client:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["Client:BaseUrl"]);

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = _clientUrl + "/callback" },
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }

    [HttpGet]
    [Route("~/callback")]
    public async Task<IActionResult> Callback()
    {
        return View();
    }

    [HttpGet]
    [Route("~/SignoutCallback")]
    public async Task<IActionResult> SignoutCallback()
    {
        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        return SignOut(
            OpenIdConnectDefaults.AuthenticationScheme,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    }
}
