using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VamoPlay.Application.Exceptions;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.CrossCutting.Auth.Entities;
using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Enums;
using VamoPlay.Domain.Interfaces;

namespace VamoPlay.Database.Seed
{
    public class DatabaseManager : IDatabaseManager
    {
        #region private members

        private VamoPlayContext _context;
        private readonly UserManager<UserIdentity> _userManager;

        #endregion

        #region constructors

        public DatabaseManager(
            VamoPlayContext context,
            UserManager<UserIdentity> userManager)
        {
            context.Database.EnsureCreated();
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region public methods implementations

        public async Task SeedTestData()
        {
            await SeedTournament();
        }

        public async Task SeedSuperAdmin()
        {
            if (!_context.UserRole.IgnoreQueryFilters().Any(ur => ur.Name.Equals(AuthenticationConstants.SuperAdministratorRoleName)))
            {
                var adminRole = new UserRole()
                {
                    Guid = Guid.NewGuid(),
                    Name = AuthenticationConstants.SuperAdministratorRoleName,
                    Description = AuthenticationConstants.SuperAdministratorRoleName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserPermissions = new List<UserClaim>()
                };

                await _context.UserRole.AddAsync(adminRole);
                await _context.SaveChangesAsync();

                if (!_context.User.IgnoreQueryFilters().Any(ua => ua.UserRoleGuid.Equals(adminRole.Guid)))
                {
                    var admin = new User
                    {
                        Name = "Admin",
                        Email = "admin@mail.com",
                        Password = "123456",
                        UserRoleGuid = adminRole.Guid,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    };

                    await RegisterUserAsync(admin);
                }
            }
        }

        public async Task SeedTournament()
        {
            if (!_context.Tournament.Any())
            {
                var tournaments = new List<Tournament>();

                var user = await _context.User.FirstOrDefaultAsync();

                for (int i = 0; i < 10; i++)
                {
                    var tournament = new Tournament
                    {
                        Guid = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Name = $"TournamentName {i}",
                        Description = $"TournamentDescription {i}",
                        Thumb = "base64here",
                        Banner = "base64here",
                        IsVisible = true,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        EndInscriptionDate = DateTime.Now,
                        StartInscriptionDate = DateTime.Now,
                        UserGuid = user.Guid,
                    };

                    tournaments.Add(tournament);
                }

                _context.AddRange(tournaments);
                _context.SaveChanges();
            }
        }

        #endregion

        #region private methods implementarions

        private async Task RegisterUserAsync(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var userIdentity = new UserIdentity
            {
                UserName = user.Email,
                User = user
            };

            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (!result.Succeeded)
                throw new VamoPlayException(result.Errors.FirstOrDefault().Description);

            _context.SaveChanges();
        }

        private string GenerateRandomString(int length, bool onlyNumbers = false, bool onlyLetters = false)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            if (onlyNumbers)
                chars = "0123456789";
            else if (onlyLetters)
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}
