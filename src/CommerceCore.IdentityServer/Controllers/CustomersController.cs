using CommerceCore.IdentityServer.Data;
using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer.Controllers;

public class CustomersController(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext dbContext
) : Controller
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpGet]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> List()
    {
        var authResult = await HttpContext.AuthenticateAsync(
            OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme
        );

        var role = authResult?.Principal?.GetClaim(Claims.Role);

        if (role == null || role != "Admin")
            return Forbid();

        var adminRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

        var adminUserIds = await _dbContext
            .UserRoles.Where(ur => ur.RoleId == adminRole.Id)
            .Select(ur => ur.UserId)
            .ToListAsync();

        var nonAdminUsers = await _userManager
            .Users.Where(u => !adminUserIds.Contains(u.Id))
            .Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email
            })
            .ToListAsync();

        return Ok(nonAdminUsers ?? []);
    }
}