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
    public class UserRoleService : BaseService, IUserRoleService
    {
        #region private members

        private readonly IUserRoleRepository _userRoleRepository;

        private readonly IUserRepository _userRepository;

        #endregion private members

        #region constructors

        public UserRoleService(
            IUserRoleRepository userRoleRepository,
            IUserRepository userRepository,
            IUnitOfWork work,
            IMapper mapper) : base(work, mapper)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        #endregion constructors

        #region public methods implementations

        public async Task<BasePagedResponseViewModel<IEnumerable<UserRoleResponseViewModel>>> GetAll(UserRoleFilter filter)
        {
            filter.Validate();

            var (roles, totalCount) = await _userRoleRepository.FindAllByAsync(filter, orderBy: r => r.Name);

            var rolesData = _mapper.Map<IEnumerable<UserRoleResponseViewModel>>(roles);

            return new BasePagedResponseViewModel<IEnumerable<UserRoleResponseViewModel>>(rolesData, filter.PageNumber, filter.PageSize, totalCount);
        }

        public async Task<UserRoleResponseViewModel> GetByGuid(Guid guid)
        {
            var role = await _userRoleRepository.GetByIdAsync(guid);

            if (role == null)
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);

            return _mapper.Map<UserRoleResponseViewModel>(role);
        }

        public async Task<UserRoleResponseViewModel> RegisterAsync(UserRoleRequestViewModel userRoleViewModel)
        {
            var userRoleDb = await _userRoleRepository.FindByAsync(c => c.Name.ToLower().Equals(userRoleViewModel.Name.ToLower()));

            if (userRoleDb != null)
                throw new VamoPlayConflictException(Resources.Resource.user_role_already_registered);

            if (!userRoleViewModel.UserPermissions.Any())
                throw new VamoPlayException(Resources.Resource.permission_is_required);

            BeginTransaction();

            var userRole = _mapper.Map<UserRole>(userRoleViewModel);
            var newUserRole = await _userRoleRepository.AddAsync(userRole);

            Commit();

            return _mapper.Map<UserRoleResponseViewModel>(newUserRole);
        }

        public async Task<UserRoleResponseViewModel> Update(Guid guid, UserRoleRequestViewModel userRoleViewModel)
        {
            var role = await _userRoleRepository.GetByIdAsync(guid);

            if (role == null)
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);

            var existentRole = await _userRoleRepository.FindByAsync((c) => !c.Guid.Equals(guid) && c.Name.Equals(userRoleViewModel.Name));

            if (existentRole != null)
                throw new VamoPlayConflictException(Resources.Resource.user_role_already_registered);

            BeginTransaction();

            var userRoleFromViewModel = _mapper.Map<UserRole>(userRoleViewModel);

            role.Name = userRoleFromViewModel.Name;
            role.Description = userRoleFromViewModel.Description;
            role.UserPermissions = userRoleFromViewModel.UserPermissions;

            await _userRoleRepository.UpdateAsync(role);

            Commit();

            return _mapper.Map<UserRoleResponseViewModel>(role);
        }

        public IEnumerable<UserClaimResponseViewModel> GetAllUserClaims()
        {
            var result = Enum.GetValues(typeof(UserClaim))
                                .Cast<UserClaim>()
                                .Select(c => c.GetUserClaimValuesAttribute())
                                .GroupBy(c => c.Module)
                                .Select(c =>
                                {
                                    var userClaimResponse = new UserClaimResponseViewModel();

                                    userClaimResponse.Module = c.Key;
                                    userClaimResponse.Permissions = c.Select(c => new UserPermissionResponseViewModel { Name = c.Name, Description = c.Description, Key = c.Key });

                                    return userClaimResponse;
                                });


            return result;
        }

        public async Task RemoveByGuid(Guid guid)
        {
            var userRole = await _userRoleRepository.GetByIdAsync(guid);

            if (userRole == null)
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);

            var userRoleRelationship = await _userRepository.FindAllByAsync(u => u.UserRoleGuid == guid);

            if (userRoleRelationship.Any())
                throw new VamoPlayConflictException(Resources.Resource.user_role_relationship);

            BeginTransaction();

            await _userRoleRepository.RemoveAsync(userRole);

            Commit();
        }

        public async Task<UserRoleResponseViewModel> GetByGuidSuperUserTrue(Guid userRoleGuid)
        {
            var role = await _userRoleRepository.GetByIdAsync(userRoleGuid, true);

            if (role == null)
            {
                throw new VamoPlayNotFoundException(Resources.Resource.user_role_not_found);
            }

            return _mapper.Map<UserRoleResponseViewModel>(role);
        }

        #endregion public methods implementations
    }
}
