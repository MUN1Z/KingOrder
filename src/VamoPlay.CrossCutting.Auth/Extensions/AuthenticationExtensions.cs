using System.Security.Claims;
using VamoPlay.CrossCutting.Auth.Constants;

namespace VamoPlay.CrossCutting.Auth.Extensions
{
    public static class AuthenticationExtensions
    {
        public static bool IsSuperAdmin(this ClaimsPrincipal user)
        {
            return user.Claims.Any(c => c.Type.ToLower().Equals(AuthenticationConstants.Role) && c.Value.Equals(AuthenticationConstants.SuperAdministratorRoleName));
        }
    }
}
