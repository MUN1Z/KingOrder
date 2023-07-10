using VamoPlay.Application.Filters;
using VamoPlay.Application.ViewModels;

namespace VamoPlay.Application.Services.Interfaces
{
    public interface IUserRoleService : IDisposable
    {
        Task<BasePagedResponseViewModel<IEnumerable<UserRoleResponseViewModel>>> GetAll(UserRoleFilter filter);
        Task<UserRoleResponseViewModel> GetByGuid(Guid guid);
        Task<UserRoleResponseViewModel> RegisterAsync(UserRoleRequestViewModel userRoleViewModel);
        Task<UserRoleResponseViewModel> Update(Guid guid, UserRoleRequestViewModel userRole);
        IEnumerable<UserClaimResponseViewModel> GetAllUserClaims();
        Task RemoveByGuid(Guid guid);
        Task<UserRoleResponseViewModel> GetByGuidSuperUserTrue(Guid userRoleGuid);
    }
}
