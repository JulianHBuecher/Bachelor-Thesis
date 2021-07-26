using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ML.Proxy
{
    public class CustomScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // IF user has not the required scope -> finish here
            var scopeClaims = context.GetClaims("scope");
            if (!scopeClaims.Any())
            {
                return Task.CompletedTask;
            }
            // Split scope string into array
            var scopes = scopeClaims.Select(c => c.Value).ToArray();

            // IF required scope is here -> succeed
            if (scopes.Any(s => s.Contains(requirement.Scope)))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
        
    }

    public class CustomAnyScopeHandler : AuthorizationHandler<HasAnyScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAnyScopeRequirement requirement)
        {
            // IF user has not the required scope -> finish here
            var scopeClaims = context.GetClaims("scope");
            if (!scopeClaims.Any())
            {
                return Task.CompletedTask;
            }
            // Split scope string into array
            var scopes = scopeClaims.Select(c => c.Value.Split(" ")).First();

            // IF required scope is here -> succeed
            if (scopes.Intersect(requirement.Scopes).Any())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
        
    }

    public static class ClaimsExtensions
    {
        public static Claim[] GetClaims(this ClaimsPrincipal user, string claimType)
        {
            var claims = user?.Claims?.Where(c => c.Type == claimType)?.ToArray();
            return claims ?? new Claim[0];
        }

        public static Claim[] GetClaims(this AuthorizationHandlerContext context, string claimType)
        {
            return context?.User?.GetClaims(claimType);
        }
    }
}
