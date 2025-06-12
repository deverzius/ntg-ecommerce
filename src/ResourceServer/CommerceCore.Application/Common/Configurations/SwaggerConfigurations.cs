namespace CommerceCore.Application.Common.Configurations;

public class SwaggerConfigurations
{
    public required string Title { get; set; }
    public required string Version { get; set; }
    public required string Description { get; set; }
    public required OAuth2Configurations OAuth2 { get; set; }

    public class OAuth2Configurations
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string AuthorizationUrl { get; set; }
        public required string RedirectUrl { get; set; }
        public required string TokenUrl { get; set; }
        public required string[][] Scopes { get; set; }
    }
}