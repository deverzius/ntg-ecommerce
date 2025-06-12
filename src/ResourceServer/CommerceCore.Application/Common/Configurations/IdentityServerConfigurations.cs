namespace CommerceCore.Application.Common.Configurations;

public class IdentityServerConfigurations
{
    public required string Authority { get; set; }
    public required string Audience { get; set; }
    public required string EncryptionKey { get; set; }
}