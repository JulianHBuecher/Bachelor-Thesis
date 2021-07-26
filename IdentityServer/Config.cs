// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IConfiguration Configuration;

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("weatherdata.read", "Reading the data from weather api."),
                new ApiScope("weatherdata.write", "Writing data to the weather api."),
                new ApiScope("weatherdata.update", "Updating data of weather api."),
                new ApiScope("weatherdata.delete", "Deleting data out of the weather api."),
                new ApiScope("locationdata.read", "Reading data from the location api."),
                new ApiScope("locationdata.write", "Writing data to the location api."),
                new ApiScope("locationdata.update", "Updating data of location api."),
                new ApiScope("locationdata.delete", "Deleting data out of the location api."),
                new ApiScope("safelist.read", "Reading data from the safelist at ML.Proxy"),
                new ApiScope("safelist.write", "Writing data to the safelist at ML.Proxy"),
                new ApiScope("safelist.delete", "Deleting data out of the safelist at ML.Proxy"),
                new ApiScope("safelist.update", "Updating data the safelist at ML.Proxy"),
                new ApiScope("blacklist.read", "Reading data from the blacklist at ML.Proxy")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>()
            {
                new ApiResource
                {
                    Name = "weatherdata",
                    DisplayName = "API for requesting weather data",
                    Scopes =
                    {
                        "weatherdata.read",
                        "weatherdata.write",
                        "weatherdata.update",
                        "weatherdata.delete"
                    },
                    // For setting the wished SigningAlgorithm
                    //AllowedAccessTokenSigningAlgorithms = { SecurityAlgorithms.RsaSsaPssSha256 }
                },
                new ApiResource
                {
                    Name = "locationdata",
                    DisplayName = "API for requesting location data",
                    Scopes =
                    {
                        "locationdata.read",
                        "locationdata.write",
                        "locationdata.update",
                        "locationdata.delete"
                    }
                },
                new ApiResource
                {
                    Name = "ml.proxy",
                    DisplayName = "Machine Learning Proxy for DoS- and DDoS-Attack Prevention",
                    Scopes = {
                        "safelist.read",
                        "safelist.write",
                        "safelist.delete",
                        "safelist.update",
                        "blacklist.read"
                    } 
                }
            };

            public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "angular-webapp",
                    ClientName = "Angular 11 Web Client",

                    AllowedGrantTypes = GrantTypes.Code,
                    // Additional Indicator for using Code Flow + PKCE
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = 
                    { 
                        Startup.StaticConfiguration.GetValue<string>("WebApp:Callback-Url"),
                        Startup.StaticConfiguration.GetValue<string>("WebApp:Silent-Refresh-Url"),
                    },
                    FrontChannelLogoutUri = Startup.StaticConfiguration.GetValue<string>("WebApp:Front-Channel-Logout-Url"),
                    PostLogoutRedirectUris = 
                    { 
                        Startup.StaticConfiguration.GetValue<string>("WebApp:Post-Logout-Redirect-Url"), 
                        Startup.StaticConfiguration.GetValue<string>("WebApp:Additional-Logout-Redirect-Url") 
                    },
                    AllowOfflineAccess = true,
                    
                    AllowedScopes = { "openid", "profile", "weatherdata.read", "locationdata.read" }
                },
                // console client for meassuring request time
                new Client
                {
                    ClientId = "Konsolen.Client",
                    ClientName = "Konsolen Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "weatherdata.read" }
                },
                // machine client for configuring safelist
                new Client
                {
                    ClientId = "Postman.Client",
                    ClientName = "Postman Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "safelist.read", "safelist.write", "safelist.delete", "safelist.update", "blacklist.read" }
                }
            };
    }
}