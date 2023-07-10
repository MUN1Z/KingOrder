using AutoMapper;
using System.Linq;
using VamoPlay.Application.Enums;
using VamoPlay.Application.Exceptions;
using VamoPlay.Application.Extensions;
using VamoPlay.Application.Filters;
using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.ViewModels;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces;
using VamoPlay.Domain.Interfaces.Repositories;

namespace VamoPlay.Application.Services
{
    public class RoleService : BaseService, IRoleService
    {
        #region private members

        private readonly IRoleRepository _roleRepository;

        private readonly IUserRepository _userRepository;

        #endregion private members

        #region constructors

        public RoleService(
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IUnitOfWork work,
            IMapper mapper) : base(work, mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        #endregion constructors

        #region public methods implementations

        public async Task<BasePagedResponseViewModel<IEnumerable<RoleResponseViewModel>>> GetAll(RoleFilter filter)
        {
            filter.Validate();

            var (roles, totalCount) = await _roleRepository.FindAllByAsync(filter, orderBy: r => r.Name);

            var rolesData = _mapper.Map<IEnumerable<RoleResponseViewModel>>(roles);

            return new BasePagedResponseViewModel<IEnumerable<RoleResponseViewModel>>(rolesData, filter.PageNumber, filter.PageSize, totalCount);
        }

        public async Task<RoleResponseViewModel> GetByGuid(Guid guid)
        {
            var role = await _roleRepository.GetByIdAsync(guid);

            if (role == null)
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);

            return _mapper.Map<RoleResponseViewModel>(role);
        }

        public async Task<RoleResponseViewModel> RegisterAsync(RoleRequestViewModel roleViewModel)
        {
            var roleDb = await _roleRepository.FindByAsync(c => c.Name.ToLower().Equals(roleViewModel.Name.ToLower()));

            if (roleDb != null)
                throw new VamoPlayConflictException(Resources.Resource.user_role_already_registered);

            if (!roleViewModel.UserPermissions.Any())
                throw new VamoPlayException(Resources.Resource.permission_is_required);

            BeginTransaction();

            var role = _mapper.Map<Role>(roleViewModel);
            var newRole = await _roleRepository.AddAsync(role);

            Commit();

            return _mapper.Map<RoleResponseViewModel>(newRole);
        }

        public async Task<RoleResponseViewModel> Update(Guid guid, RoleRequestViewModel roleViewModel)
        {
            var role = await _roleRepository.GetByIdAsync(guid);

            if (role == null)
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);

            var existentRole = await _roleRepository.FindByAsync((c) => !c.Guid.Equals(guid) && c.Name.Equals(roleViewModel.Name));

            if (existentRole != null)
                throw new VamoPlayConflictException(Resources.Resource.user_role_already_registered);

            BeginTransaction();

            var roleFromViewModel = _mapper.Map<Role>(roleViewModel);

            role.Name = roleFromViewModel.Name;
            role.Description = roleFromViewModel.Description;
            role.UserPermissions = roleFromViewModel.UserPermissions;

            await _roleRepository.UpdateAsync(role);

            Commit();

            return _mapper.Map<RoleResponseViewModel>(role);
        }

        public IEnumerable<ClaimResponseViewModel> GetAllUserClaims()
        {
            var result = Enum.GetValues(typeof(ClaimType))
                                .Cast<ClaimType>()
                                .Select(c => c.GetUserClaimValuesAttribute())
                                .GroupBy(c => c.Module)
                                .Select(c =>
                                {
                                    var userClaimResponse = new ClaimResponseViewModel();

                                    userClaimResponse.Module = c.Key;
                                    userClaimResponse.Permissions = c.Select(c => new UserPermissionResponseViewModel { Name = c.Name, Description = c.Description, Key = c.Key });

                                    return userClaimResponse;
                                });


            return result;
        }

        public async Task RemoveByGuid(Guid guid)
        {
            var role = await _roleRepository.FindByAsync(c => c.Guid == guid, c => c.Users);

            if (role == null)
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);

            if (role.Users.Any())
                throw new VamoPlayConflictException(Resources.Resource.user_role_relationship);

            BeginTransaction();

            await _roleRepository.RemoveAsync(role);

            Commit();
        }

        public async Task<RoleResponseViewModel> GetByGuidSuperUserTrue(Guid roleGuid)
        {
            var role = await _roleRepository.GetByIdAsync(roleGuid, true);

            if (role == null)
            {
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);
            }

            return _mapper.Map<RoleResponseViewModel>(role);
        }

        #endregion public methods implementations
    }
}
