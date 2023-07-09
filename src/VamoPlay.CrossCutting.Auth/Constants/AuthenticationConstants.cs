namespace VamoPlay.CrossCutting.Auth.Constants
{
    public static class AuthenticationConstants
    {
        public const string Role = "role";
        public const string SuperAdministratorRoleName = "SuperAdministrator";
        public const string Authorization = "Authorization";
        public const string Unauthorized = "Unauthorized";
        public const string Bearer = "Bearer";
        
        /* JWT */

        public const string JWT_OPTIONS = "JwtIssuerOptions";
        public const string JWT_EXPIRES_MINUTES_OPTION = "JwtExpireMinutes";
        public const string JWT_KEY_OPTION = "JwtKey";
        public const string JWT_ISSUER_OPTION = "JwtIssuer";
        public const string JWT_AUDIENCE_OPTION = "JwtAudience";
        public const string JWT_USER_TYPE_PROPERTY = "type";
        public const string JWT_USER_BUY_FROM_SUPPLIER_PROPERTY = "buyFromSupplier";
        public const string JWT_CLAIMS_PROPERTY = "claims";
    }
}
