using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceCore.IdentityServer.Configurations;

public class IdentityServerConfigurations
{
    public required string EncryptionKey { get; set; }
    public required ClientConfigurations Client { get; set; }
    public required ServerConfigurations Server { get; set; }

    public class ClientConfigurations
    {
        public required string[] RedirectionEndpointUris { get; set; }
    }

    public class ServerConfigurations
    {
        public required string[] AuthorizationEndpointUris { get; set; }
        public required string[] EndSessionEndpointUris { get; set; }
        public required string[] TokenEndpointUris { get; set; }
        public required string[] UserInfoEndpointUris { get; set; }
    }
}