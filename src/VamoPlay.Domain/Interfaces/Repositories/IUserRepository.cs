using System.Linq.Expressions;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Shared.Interfaces;

namespace VamoPlay.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindWithRoles(string email);

        Task<(IEnumerable<User>, int)> FindAllWithoutAdmin<TFilter>(TFilter filter, Expression<Func<User, object>> orderBy = null, params Expression<Func<User, object>>[] includeProperties) where TFilter : IFilter;

        Task<User> GetByEmailAsync(string email, bool ignoreQueryFilter = false, params Expression<Func<User, object>>[] includeProperties);
    }
}
