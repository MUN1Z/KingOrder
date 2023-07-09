using AutoMapper;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Domain.Entities;

namespace VamoPlay.Application.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestViewModel, User>();
            CreateMap<UserResponseViewModel, User>();

            CreateMap<User, UserRequestViewModel>();
            CreateMap<User, UserResponseViewModel>();

            CreateMap<UserRequestViewModel, UserResponseViewModel>();
            CreateMap<UserResponseViewModel, UserRequestViewModel>();
        }
    }
}
