using VamoPlay.CrossCutting.Auth.Attributes;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.CrossCutting.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace VamoPlay.CrossCutting.Auth.Providers
{
    public class AuthorizedPolicyProvider : IAuthorizationPolicyProvider
    {
        public AuthorizedPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder(AuthenticationConstants.Bearer).RequireAuthenticatedUser().Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (string.IsNullOrWhiteSpace(policyName))
            {
                return FallbackPolicyProvider.GetPolicyAsync(policyName);
            }

            var policyTokens = policyName.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (policyTokens?.Any() != true)
            {
                return FallbackPolicyProvider.GetPolicyAsync(policyName);
            }

            var policy = new AuthorizationPolicyBuilder(AuthenticationConstants.Bearer);
            var identifier = Guid.NewGuid();

            foreach (var token in policyTokens)
            {
                var pair = token.Split('$', StringSplitOptions.RemoveEmptyEntries);

                if (pair?.Any() != true || pair.Length != 2)
                {
                    return FallbackPolicyProvider.GetPolicyAsync(policyName);
                }

                IAuthorizationRequirement requirement = (pair[0]) switch
                {
                    AuthorizedAttribute.ClaimsGroup => new ClaimsRequirement(pair[1], identifier),
                    _ => null,
                };

                if (requirement == null)
                {
                    return FallbackPolicyProvider.GetPolicyAsync(policyName);
                }

                policy.AddRequirements(requirement);
            }

            return Task.FromResult(policy.Build());
        }
    }
}
