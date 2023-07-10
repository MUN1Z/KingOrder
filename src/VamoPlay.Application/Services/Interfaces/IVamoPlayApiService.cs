using VamoPlay.Application.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace VamoPlay.Application.Services.Interfaces
{
    public interface IVamoPlayApiService
    {
        Task<IEnumerable<TournamentResponseViewModel>> GetTournamentsAsync();
        Task<TournamentResponseViewModel> GetTournamentByGuidAsync(Guid guid);
        Task<TournamentResponseViewModel> GetTournamentByGuidAsync(string guid);
        Task<TournamentResponseViewModel> CreateTournament(TournamentRequestViewModel tournament);

        Task<TournamentResponseViewModel> FavoriteTournament(Guid guid);

        Task<TournamentResponseViewModel> UpdateTournament(Guid guid, TournamentRequestViewModel tournament);

        Task<bool> DeleteTournament(Guid guid);
    }
}
