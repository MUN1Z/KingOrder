using AutoMapper;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Domain.Entities;

namespace VamoPlay.Application.AutoMapperProfiles
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<TournamentRequestViewModel, Tournament>();
            CreateMap<TournamentResponseViewModel, Tournament>();

            CreateMap<Tournament, TournamentRequestViewModel>();
            CreateMap<Tournament, TournamentResponseViewModel>();

            CreateMap<TournamentRequestViewModel, TournamentResponseViewModel>();
            CreateMap<TournamentResponseViewModel, TournamentRequestViewModel>();
        }
    }
}
