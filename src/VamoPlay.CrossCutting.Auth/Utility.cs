using Microsoft.AspNetCore.Authorization;

namespace VamoPlay.CrossCutting.Auth
{
    public static class Utility
    {
        public static void Succeed(AuthorizationHandlerContext context, Guid identifier)
        {
            var groupedRequirements = context.Requirements.Where(r => (r as IIdentifiable)?.Identifier == identifier);

            foreach (var requirement in groupedRequirements)
            {
                context.Succeed(requirement);
            }
        }

        public static void Fail(AuthorizationHandlerContext context)
        {
            context.Fail();
        }
    }
}
