using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthIS4_Core3_1.IdentityServer4Service
{
    public static class AuthConfiguration
    {
        internal static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        internal static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "OrdersApi" },
                },
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {
                        "OrdersApi",
                        "ClientMvc" ,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RedirectUris = { "https://localhost:2001/signin-oidc"},
                    RequireConsent = false
                },
                 new Client
                {
                    ClientId = "client_id_js",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    AllowedCorsOrigins = { "https://localhost:9001" },
                    RedirectUris = { "https://localhost:9001/callback.html", "https://localhost:9001/refresh.html"},
                    PostLogoutRedirectUris = new List<string> { "https://localhost:9001/index.html" },
                    AllowedScopes = {
                        "OrdersApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                }
            };

        internal static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("OrdersApi"),
            };

        internal static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("OrdersApi"),
            };
    }
}
