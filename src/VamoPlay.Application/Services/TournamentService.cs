using AutoMapper;
using VamoPlay.Application.Comparers;
using VamoPlay.Application.Exceptions;
using VamoPlay.Application.Filters;
using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces;
using VamoPlay.Domain.Interfaces.Repositories;

namespace VamoPlay.Application.Services
{
    public class TournamentService : BaseService, ITournamentService
    {
        #region private members

        private readonly ITournamentRepository _tournamentRepository;

        #endregion private members

        #region constructors

        public TournamentService(
            ITournamentRepository tournamentRepository,
            IUnitOfWork work,
            IMapper mapper) : base(work, mapper)
        {
            _tournamentRepository = tournamentRepository;
        }

        #endregion constructors

        #region public methods implementations

        public async Task<IEnumerable<TournamentResponseViewModel>> GetAll(TournamentFilter filter)
        {
            filter.Validate();

            var tournaments = new List<TournamentResponseViewModel>();

            var (tournamentsDb, totalCountDb) = await _tournamentRepository.FindAllByAsync(filter, hasPagination: false);

            //var tournamentsDb = await _tournamentRepository.GetAllAsync();

            tournaments = _mapper.Map<IEnumerable<TournamentResponseViewModel>>(tournamentsDb).ToList();
           
            return tournaments;
        }

        public async Task<TournamentResponseViewModel> GetByGuid(Guid guid)
        {
            var tournamentDb = await _tournamentRepository.GetByIdAsync(guid);

            if (tournamentDb == null)
                throw new VamoPlayException("Tournament not found!");

            return _mapper.Map<TournamentResponseViewModel>(tournamentDb);
        }

        public async Task<TournamentResponseViewModel> Create(TournamentRequestViewModel tournamentViewModel)
        {
            BeginTransaction();

            var existentTournament = await _tournamentRepository.FindByAsync(c => c.Name == tournamentViewModel.Name);

            if (existentTournament != null)
                throw new VamoPlayException("Tournament with same Gtin already registered!");

            var tournament = _mapper.Map<Tournament>(tournamentViewModel);

            await _tournamentRepository.AddAsync(tournament);

            Commit();

            return _mapper.Map<TournamentResponseViewModel>(tournament);
        }

        public async Task<bool> Delete(Guid guid)
        {
            var tournamentDb = await _tournamentRepository.GetByIdAsync(guid);

            if (tournamentDb == null)
                throw new VamoPlayException("Tournament not found!");

            BeginTransaction();

            await _tournamentRepository.RemoveAsync(tournamentDb);

            Commit();

            return true;
        }

        public async Task<TournamentResponseViewModel> Update(Guid guid, TournamentRequestViewModel tournamentViewModel)
        {
            var tournamentDb = await _tournamentRepository.GetByIdAsync(guid);

            if (tournamentDb == null)
                throw new VamoPlayException("Tournament not found!");

            var existentTournament = await _tournamentRepository.FindByAsync(c => c.Name == tournamentViewModel.Name && c.Guid != guid);

            if (existentTournament != null)
                throw new VamoPlayException("Another tournament with same Name already registered!");

            BeginTransaction();

            await _tournamentRepository.UpdateAsync(tournamentDb);

            Commit();

            return _mapper.Map<TournamentResponseViewModel>(tournamentDb);
        }

        #endregion public methods implementations
    }
}
