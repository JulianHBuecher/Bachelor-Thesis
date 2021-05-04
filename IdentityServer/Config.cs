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
                new ApiScope("locationdata.delete", "Deleting data out of the location api.")
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
            };
    }
}