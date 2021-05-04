using Microsoft.AspNetCore.Authorization;
using System;

namespace LocationApi
{
    public static class AuthExtension
    {
        public static AuthorizationPolicyBuilder RequireScope(this AuthorizationPolicyBuilder policy, string scope)
        {
            policy.Requirements.Add(new HasScopeRequirement(scope));
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
}
