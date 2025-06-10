using System.ComponentModel.DataAnnotations;

namespace CommerceCore.IdentityServer.Models;

public class LoginInput
{
    [Required][EmailAddress] public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
}