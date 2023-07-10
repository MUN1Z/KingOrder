using Microsoft.AspNetCore.Authorization;

namespace VamoPlay.CrossCutting.Auth.Requirements
{
    public class ClaimsRequirement : IAuthorizationRequirement, IIdentifiable
    {
        public ClaimsRequirement(string claims, Guid identifier)
        {
            Claims = claims;
            Identifier = identifier;
        }

        public string Claims { get; }

        public Guid Identifier { get; set; }
    }
}
