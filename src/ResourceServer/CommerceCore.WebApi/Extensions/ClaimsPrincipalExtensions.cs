using System.Security.Claims;
using CommerceCore.Shared.Exceptions;

namespace CommerceCore.WebAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid ExtractUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst("sub")?.Value;
        if (userId is null)
        {
            throw new AppException(400, "User ID claim not found in the token");
        }

        var isValidGuid = Guid.TryParse(userId, out var parsedUserId);
        if (!isValidGuid)
        {
            throw new AppException(400, "User ID claim is not a valid GUID");
        }

        return parsedUserId;
    }
}
