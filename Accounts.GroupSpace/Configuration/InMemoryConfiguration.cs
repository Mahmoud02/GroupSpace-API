using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Accounts.GroupSpace.Configuration
{
    public static class InMemoryConfiguration
    {

        public static IEnumerable<Client> Clients => new[] {
            
            new Client
            {
                ClientId = "groupSpaceSPA",
                ClientName = "SPA  Client",
                RequireClientSecret = false,
                AllowedGrantTypes = new[]{ GrantType.AuthorizationCode },
                RequirePkce = true,
                AllowOfflineAccess =true,
                //used to ensure icluding  claims
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowedScopes = new[] {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "roles",
                    "GroupSpaceApiScope",
                },
                RedirectUris = new List<string>
                {
                    "http://localhost:4200/login",                  
                },
                PostLogoutRedirectUris = new List<string>
                {
                    "http://localhost:4200"

                },
                AllowedCorsOrigins = new List<string>
                    {

                      "http://localhost:4200"
                    },
            },
            new Client
            {
                /*AuthorizationCodeLifetime = ,
                IdentityTokenLifetime = ,
                AbsoluteRefreshTokenLifetime = ,
                AccessTokenLifetime = 100,*/
                AllowOfflineAccess =true,
                UpdateAccessTokenClaimsOnRefresh = true,
                ClientId = "groupSpaceAdmin",
                ClientSecrets = new []{ new Secret("groupSpaceAdminKey".Sha256()) },
                AllowedGrantTypes = new[]{ GrantType.AuthorizationCode },
                RedirectUris = new []{
                    "https://localhost:44383/signin-oidc",
                },
                PostLogoutRedirectUris = {
                    "https://localhost:44383/signout-callback-oidc"
                },
                AlwaysIncludeUserClaimsInIdToken = true,
                RequirePkce = true, 
                AllowedScopes = new[] {                 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                     IdentityServerConstants.StandardScopes.Email,
                    "roles",
                    "GroupSpaceApiScope",                  
                }
            },

        };
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Email(),
              new IdentityResource(
                  "roles",
                  "Your Roles",
                   new List<string>(){ JwtClaimTypes.Role}  
                  )
            };
         public static IEnumerable<ApiResource> ApiResources => new [] {
            new ApiResource {
                Name = "groupSpaceApi",
                DisplayName = "Group Space Api",
               // claims to include in access token
                UserClaims =
                {      
                    JwtClaimTypes.Role,
                },
 
                // API  scopes
                Scopes =
                {
                    "GroupSpaceApiScope"
                }
            }
                          
        };
       
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope(name: "GroupSpaceApiScope",   displayName: "Read,Write,Delete data.")      
            };
        }
        
    }
}
