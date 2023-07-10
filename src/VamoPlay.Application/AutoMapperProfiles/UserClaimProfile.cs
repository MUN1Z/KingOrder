using AutoMapper;
using UserClaimDomain = VamoPlay.Domain.Enums.ClaimType;
using UserClaimApplication = VamoPlay.Application.Enums.ClaimType;

namespace VamoPlay.Application.AutoMapperProfiles
{
    public class UserClaimProfile : Profile
    {
        #region constructors

        public UserClaimProfile()
        {
            CreateMap<UserClaimDomain, UserClaimApplication>();
            CreateMap<UserClaimApplication, UserClaimDomain>();
        }

        #endregion
    }
}
