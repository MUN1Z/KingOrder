using AutoMapper;
using VamoPlay.Application.ViewModels;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Domain.Entities;

namespace CISOP.Application.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {
        #region constructors

        public RoleProfile()
        {
            CreateMap<Role, RoleRequestViewModel>();
            CreateMap<Role, RoleResponseViewModel>();

            CreateMap<RoleRequestViewModel, Role>();
            CreateMap<RoleResponseViewModel, Role>();

            CreateMap<Role, SelectDataResponseViewModel>();
        }

        #endregion
    }
}
