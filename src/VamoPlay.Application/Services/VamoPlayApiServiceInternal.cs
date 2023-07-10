using VamoPlay.Application.Extensions;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Application.Services.Interfaces;

namespace VamoPlay.Application.Services
{
    public class VamoPlayApiServiceInternal : IVamoPlayApiService
    {
        #region Private Members

        //private readonly string _url = "http://10.0.2.2:5032/api";
        private readonly string _url = "https://kingorderapi.azurewebsites.net/api";
        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        public VamoPlayApiServiceInternal()
        {
            _httpClient = new HttpClient();
        }

        #endregion

        #region Public Methods 

        public async Task<IEnumerable<TournamentResponseViewModel>> GetTournamentsAsync()
        {
            var requestUri = _url + "/tournament";
            return await _httpClient.GetAndDeserialize <IEnumerable<TournamentResponseViewModel>> (requestUri);
        }

        public async Task<TournamentResponseViewModel> GetTournamentByGuidAsync(Guid guid)
            => await GetTournamentByGuidAsync(guid.ToString());

        public async Task<TournamentResponseViewModel> GetTournamentByGuidAsync(string guid)
        {
            var requestUri = _url + $"/tournament/{guid}";
            return await _httpClient.GetAndDeserialize<TournamentResponseViewModel>(requestUri);
        }

        public async Task<TournamentResponseViewModel> CreateTournament(TournamentRequestViewModel tournament)
        {
            var requestUri = _url + $"/tournament";
            return await _httpClient.PostAndDeserialize<TournamentResponseViewModel>(requestUri, tournament);
        }

        public async Task<TournamentResponseViewModel> FavoriteTournament(Guid guid)
        {
            var requestUri = _url + $"/tournament/favorite/{guid}";
            return await _httpClient.PutAndDeserialize<TournamentResponseViewModel>(requestUri, null);
        }

        public async Task<TournamentResponseViewModel> UpdateTournament(Guid guid, TournamentRequestViewModel tournament)
        {
            var requestUri = _url + $"/tournament/{guid}";
            return await _httpClient.PutAndDeserialize<TournamentResponseViewModel>(requestUri, tournament);
        }

        public async Task<bool> DeleteTournament(Guid guid)
        {
            var requestUri = _url + $"/tournament/{guid}";
            return await _httpClient.DeleteAndDeserialize<bool>(requestUri);
        }

        #endregion
    }
}