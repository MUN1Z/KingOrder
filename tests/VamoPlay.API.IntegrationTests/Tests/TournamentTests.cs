using FluentAssertions;
using VamoPlay.Application.Filters;
using VamoPlay.Application.Extensions;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace VamoPlay.API.IntegrationTests.Tests
{
    public class TournamentTests : BaseIntegrationTests
    {
        #region Constructors

        public TournamentTests() { }

        #endregion

        #region Get Tests

        [Fact(DisplayName = "Get All Tournaments")]
        public async Task Get_All_Tournaments()
        {
            // Arrange
            var tournamentsCount = await _vamoPlayContext.Tournament.CountAsync();
            var filter = new TournamentFilter();

            // Act
            var response =
                await _vamoPlayHttpClient.GetAndDeserialize<IEnumerable<TournamentResponseViewModel>>(filter.ToQueryString($"Tournament/"));

            //Assert
            response.Count().Should().Be(tournamentsCount);
        }

        [Fact(DisplayName = "Get Tournament By Guid")]
        public async Task Get_Tournament_By_Guid()
        {
            // Arrange
            var tournament = await CreateTournament();

            // Act
            var response =
                await _vamoPlayHttpClient.GetAndDeserialize<TournamentResponseViewModel>($"api/Tournament/{tournament.Guid}");

            //Assert
            response.Name.Should().Be(tournament.Name);
        }

        #endregion

        #region Put Tests

        [Fact(DisplayName = "Create Tournament")]
        public async Task Create_Tournament()
        {
            // Arrange
            var tournament = new TournamentRequestViewModel
            {
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                Thumb = GenerateRandomString(255)
            };

            // Act
            var response =
                await _vamoPlayHttpClient.PostAndDeserialize<TournamentResponseViewModel>("api/Tournament", tournament);

            var tournamentDb = await _vamoPlayContext.Tournament.FirstOrDefaultAsync(c => c.Guid == response.Guid);

            //Assert
            response.Name.Should().Be(tournamentDb.Name);
        }

        #endregion

        #region Put Tests

        [Fact(DisplayName = "Update Tournament")]
        public async Task Update_Tournament()
        {
            // Arrange
            var tournament = new TournamentRequestViewModel
            {
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                Thumb = GenerateRandomString(255)
            };

            // Act
            var responseCreate =
                await _vamoPlayHttpClient.PostAndDeserialize<TournamentResponseViewModel>("api/Tournament", tournament);

            tournament.Name += " - Updated";
            tournament.Description = GenerateRandomString(10);

            var responseUpdate =
                await _vamoPlayHttpClient.PutAndDeserialize<TournamentResponseViewModel>($"api/Tournament/{responseCreate.Guid}", tournament);

            var tournamentDb = await _vamoPlayContext.Tournament.FirstOrDefaultAsync(c => c.Guid == responseCreate.Guid);

            //Assert
            responseUpdate.Name.Should().Be(tournamentDb.Name);
            responseUpdate.Description.Should().Be(tournamentDb.Description);
        }

        #endregion

        #region Delete Tests

        [Fact(DisplayName = "Delete Tournament")]
        public async Task Delete_Tournament()
        {
            // Arrange
            var tournament = new TournamentRequestViewModel
            {
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                Thumb = GenerateRandomString(255)
            };

            // Act
            var responseCreate =
                await _vamoPlayHttpClient.PostAndDeserialize<TournamentResponseViewModel>("api/Tournament", tournament);

            var responseDelete =
                await _vamoPlayHttpClient.DeleteAndDeserialize<bool>($"api/Tournament/{responseCreate.Guid}");

            var tournamentDb = await _vamoPlayContext.Tournament.FirstOrDefaultAsync(c => c.Guid == responseCreate.Guid);

            //Assert
            responseDelete.Should().Be(true);
            tournamentDb.Should().BeNull();
        }

        #endregion
    }
}
