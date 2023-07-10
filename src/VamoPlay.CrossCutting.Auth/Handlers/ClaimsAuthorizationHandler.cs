using System.Security.Claims;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.CrossCutting.Auth.Extensions;
using VamoPlay.CrossCutting.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace VamoPlay.CrossCutting.Auth.Handlers
{
    public class ClaimsAuthorizationHandler : AuthorizationHandler<ClaimsRequirement>
    {
        private readonly ILogger<ClaimsAuthorizationHandler> _logger;

        public ClaimsAuthorizationHandler(ILogger<ClaimsAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated || context.HasSucceeded || requirement == null || string.IsNullOrWhiteSpace(requirement.Claims))
            {
                return Task.CompletedTask;
            }

            if (context.User.IsSuperAdmin())
            {
                Utility.Succeed(context, requirement.Identifier);
                return Task.CompletedTask;
            }

            var expectedRequirements = requirement.Claims.Split("|", StringSplitOptions.RemoveEmptyEntries);

            if (expectedRequirements?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var userPermissionClaims = context.User.Claims?.Where(c =>
                string.Equals(c.Type, AuthenticationConstants.JWT_CLAIMS_PROPERTY, StringComparison.OrdinalIgnoreCase));

            if (userPermissionClaims == null || !userPermissionClaims.Any())
            {
                return Task.CompletedTask;
            }

            var match = userPermissionClaims.Any(upc => expectedRequirements.Contains(upc.Value));

            if (match)
            {
                Utility.Succeed(context, requirement.Identifier);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
