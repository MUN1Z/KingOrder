using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace VamoPlay.CrossCutting.Auth.Handlers
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private ILogger<AuthenticationHandler> _logger;
        private IUserRepository _userAccountRepository;

        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserRepository userAccountRepository) : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger<AuthenticationHandler>();
            _userAccountRepository = userAccountRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthenticationConstants.Authorization))
                return AuthenticateResult.Fail(AuthenticationConstants.Unauthorized);

            string authorizationHeader = Request.Headers[AuthenticationConstants.Authorization];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith(AuthenticationConstants.Bearer, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail(AuthenticationConstants.Unauthorized);
            }

            string token = authorizationHeader.Substring(AuthenticationConstants.Bearer.Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail(AuthenticationConstants.Unauthorized);
            }

            try
            {
                return await ValidateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private async Task<AuthenticateResult> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

            var claims = securityToken.Claims;

            var id = claims.FirstOrDefault().Value;

            var user = await _userAccountRepository.FindByAsync(c => c.Guid.ToString().Equals(id));
            if (user == null || DateTime.UtcNow > securityToken.ValidTo || !user.IsActive || user.IsDeleted)
                return AuthenticateResult.Fail(AuthenticationConstants.Unauthorized);

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
