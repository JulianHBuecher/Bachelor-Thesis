using Microsoft.AspNetCore.Authorization;
using System;

namespace ML.Proxy
{
    public static class AuthExtension
    {
        public static AuthorizationPolicyBuilder RequireScope(this AuthorizationPolicyBuilder policy, string scope)
        {
            policy.Requirements.Add(new HasScopeRequirement(scope));
            return policy;
        }
        
        public static AuthorizationPolicyBuilder RequireScopeEither(this AuthorizationPolicyBuilder policy, params string[] scopes)
        {
            policy.Requirements.Add(new HasAnyScopeRequirement(scopes));
            return policy;
        }
    }


    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }

        public HasScopeRequirement(string scope)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }

    public class HasAnyScopeRequirement : IAuthorizationRequirement
    {
        public string[] Scopes { get; }

        public HasAnyScopeRequirement(string[] scopes)
        {
            Scopes = scopes ?? throw new ArgumentNullException(nameof(scopes));
        }
    }
}
