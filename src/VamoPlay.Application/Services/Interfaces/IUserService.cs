using VamoPlay.Application.Filters;
using VamoPlay.Application.ViewModels.Response;

namespace VamoPlay.Application.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<UserResponseViewModel> RegisterAsync(UserRequestViewModel obj);

        Task<LoginResponseViewModel> LoginAsync(LogInRequestViewModel loginViewModel);

        Task LogOffAsync();
    }
}
