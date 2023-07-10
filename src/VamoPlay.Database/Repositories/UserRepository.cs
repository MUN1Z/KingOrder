using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces.Repositories;
using VamoPlay.Domain.Shared.Interfaces;

namespace VamoPlay.Database.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region constructors

        public UserRepository(VamoPlayContext context) : base(context) { }

        #endregion constructors

        #region public methods implementations

        public async Task<User> FindWithRoles(string email)
        {
            var queryable = Db.Set<User>().AsNoTracking().AsQueryable().IgnoreQueryFilters().Include(c => c.Roles);
            return await queryable.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<(IEnumerable<User>, int)> FindAllWithoutAdmin<TFilter>(TFilter filter, Expression<Func<User, object>> orderBy = null, params Expression<Func<User, object>>[] includeProperties) where TFilter : IFilter
        {
            var adminRole = Db.Set<Role>().IgnoreQueryFilters().FirstOrDefault(c => c.Name.Equals(AuthenticationConstants.SuperAdministratorRoleName));
            var queryable = Db.Set<User>().AsQueryable().Include(c => c.Roles).Where(u => !u.Roles.Any(c => c.Guid.Equals(adminRole.Guid)));

            return await FindAllByAsync(filter, predicate: null, queryable, orderBy, hasPagination: true, includeProperties);
        }

        public virtual async Task<User> GetByEmailAsync(
            string email, bool ignoreQueryFilter = false,
            params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> queryable = Db.Set<User>().AsNoTracking().AsQueryable();

            if (ignoreQueryFilter)
                queryable = queryable.IgnoreQueryFilters();

            foreach (Expression<Func<User, object>> includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return await queryable.FirstOrDefaultAsync(c => c.Email == email);
        }

        #endregion public methods implementations
    }
}
