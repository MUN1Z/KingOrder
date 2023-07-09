using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.Exceptions;
using VamoPlay.Application.Resources;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.CrossCutting.Auth.Entities;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces;
using VamoPlay.Domain.Interfaces.Repositories;

namespace VamoPlay.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        #region private members

        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        private readonly UserManager<UserIdentity> _userManager;

        private readonly SignInManager<UserIdentity> _signInManager;

        #endregion private members

        #region constructors

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork work,
            IMapper mapper,
            IConfiguration configuration,
            UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager) : base(work, mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        #endregion constructors

        #region public methods implementations


        public async Task<LoginResponseViewModel> LoginAsync(LogInRequestViewModel loginViewModel)
        {
            //var userDb = await _userRepository.GetByEmailAsync(loginViewModel.Email, true);
            var userDb = await _userRepository.FindByAsync(c => c.Email.Equals(loginViewModel.Email));

            var user = await _userManager.FindByNameAsync(loginViewModel.Email);

            var isUnauthorized = userDb == null || user == null || !BCrypt.Net.BCrypt.Verify(loginViewModel.Password, userDb.Password);

            if (isUnauthorized)
                throw new VamoPlayUnauthorizedException(Resource.invalid_email_or_password);

            if (!userDb.IsActive)
                throw new VamoPlayUnauthorizedException(Resource.user_disabled);

            await _signInManager.SignInAsync(user, isPersistent: false);

            BeginTransaction();

            userDb.LastAccess = DateTime.UtcNow;

            await _userRepository.UpdateAsync(userDb);

            Commit();

            return await GenerateUserToken(userDb);
        }

        public async Task LogOffAsync() => await _signInManager.SignOutAsync();

        public async Task<UserResponseViewModel> RegisterAsync(UserRequestViewModel userViewModel)
        {
            var userDb = await _userRepository.FindByAsync(c => c.Email.Equals(userViewModel.Email));

            if (userDb != null)
                throw new VamoPlayConflictException(Resource.email_already_used);

            BeginTransaction();

            var user = _mapper.Map<User>(userViewModel);

            user.IsActive = true;

            //var userPassword = GenerateRandomPassword();
            user.Password = BCrypt.Net.BCrypt.HashPassword(userViewModel.Password);

            //var userRole = await _userRoleRepository.GetByIdAsync(userViewModel.UserRoleGuid);

            //if (userRole == null)
            //    throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);

            var newUser = await _userRepository.AddAsync(user);

            var userIdentity = new UserIdentity
            {
                UserName = user.Email,
                User = user
            };

            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (!result.Succeeded)
                throw new VamoPlayException(result.Errors.FirstOrDefault().Description);

            //await _emailSenderService.SendEmailRegisterUser(userIdentity.User.Email, userPassword, userIdentity.User.Name, Resources.Resource.user_create);

            Commit();

            return _mapper.Map<UserResponseViewModel>(newUser);
        }

        #endregion public methods implementations

        #region private methods implementations

        private string? GetIdCurrentUser()
        {
            return _signInManager.Context.User.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
        }

        private async Task<LoginResponseViewModel> GenerateUserToken(User userDb)
        {
            var jwtAppSettingOptions = _configuration.GetSection(AuthenticationConstants.JWT_OPTIONS);
            var time = DateTime.UtcNow.Add(TimeSpan.FromMinutes(
                Convert.ToDouble(jwtAppSettingOptions[AuthenticationConstants.JWT_EXPIRES_MINUTES_OPTION])));

            var handler = new JwtSecurityTokenHandler();

            var signingKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtAppSettingOptions[AuthenticationConstants.JWT_KEY_OPTION]));

            var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, userDb.Guid.ToString()),
                    new Claim(ClaimTypes.Name, userDb.Name),
                    new Claim(ClaimTypes.Email, userDb.Email),
            };

            //claims.Add(new Claim(ClaimTypes.Role, userDb.UserRole.Name));
            //claims.Add(new Claim(AuthenticationConstants.JWT_USER_TYPE_PROPERTY,
            //    Convert.ToInt32(userDb.UserAccountType).ToString(), ClaimValueTypes.Integer));
            //claims.Add(new Claim(AuthenticationConstants.JWT_USER_BUY_FROM_SUPPLIER_PROPERTY,
            //    userDb.ResaleGuid != null
            //        ? userDb.Resale.BuyFromSupplier.ToString()
            //        : false.ToString(), ClaimValueTypes.Boolean));

            //if (userDb.UserRole.Name == AuthenticationConstants.SuperAdministratorRoleName)
            //{
            //    foreach (var permission in ((UserClaim[])Enum.GetValues(typeof(UserClaim))).ToList())
            //        claims.Add(new Claim(AuthenticationConstants.JWT_CLAIMS_PROPERTY, permission.ToString()));
            //}
            //else
            //    foreach (var permission in userDb.UserRole.UserPermissions)
            //        claims.Add(new Claim(AuthenticationConstants.JWT_CLAIMS_PROPERTY, permission.ToString()));

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtAppSettingOptions[AuthenticationConstants.JWT_ISSUER_OPTION],
                Audience = jwtAppSettingOptions[AuthenticationConstants.JWT_AUDIENCE_OPTION],
                IssuedAt = DateTime.UtcNow,
                Expires = time,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var token = handler.CreateToken(securityTokenDescriptor);

            var tokenString = handler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = tokenString,
                CreatedIn = DateTime.Now,
                ExpiresIn = time
            };

            return response;
        }

        private string GenerateRandomPassword()
        {
            var passwordOptions = _userManager.Options.Password;

            var opts = new PasswordOptions()
            {
                RequiredLength = passwordOptions.RequiredLength,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$"                           // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        #endregion
    }
}
