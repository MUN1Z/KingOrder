using VamoPlay.Application.Filters;
using VamoPlay.Application.ViewModels;

namespace VamoPlay.Application.Services.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<BasePagedResponseViewModel<IEnumerable<RoleResponseViewModel>>> GetAll(RoleFilter filter);
        Task<RoleResponseViewModel> GetByGuid(Guid guid);
        Task<RoleResponseViewModel> RegisterAsync(RoleRequestViewModel roleViewModel);
        Task<RoleResponseViewModel> Update(Guid guid, RoleRequestViewModel role);
        IEnumerable<ClaimResponseViewModel> GetAllUserClaims();
        Task RemoveByGuid(Guid guid);
        Task<RoleResponseViewModel> GetByGuidSuperUserTrue(Guid roleGuid);
    }
}
