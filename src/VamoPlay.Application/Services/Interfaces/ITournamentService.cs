using VamoPlay.Application.Filters;
using VamoPlay.Application.ViewModels.Response;

namespace VamoPlay.Application.Services.Interfaces
{
    public interface ITournamentService : IDisposable
    {
        Task<IEnumerable<TournamentResponseViewModel>> GetAll(TournamentFilter filter);
        Task<TournamentResponseViewModel> GetByGuid(Guid guid);
        Task<TournamentResponseViewModel> Create(TournamentRequestViewModel tournamentViewModel);
        Task<bool> Delete(Guid guid);
        Task<TournamentResponseViewModel> Update(Guid guid, TournamentRequestViewModel tournamentViewModel);
    }
}
