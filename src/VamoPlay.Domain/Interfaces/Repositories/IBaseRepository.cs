using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Shared.Interfaces;
using System.Linq.Expressions;

namespace VamoPlay.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity obj);

        Task<TEntity> GetFirstAsync();

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> orderBy = null);

        Task<TEntity> GetByIdAsync(Guid id, bool ignoreQueryFilter);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includeProperties);

        Task UpdateAsync(TEntity obj, params Expression<Func<TEntity, object>>[] includeProperties);

        Task RemoveAsync(TEntity obj);

        Task SoftRemoveAsync(TEntity obj);

        Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(TFilter filter, bool hasPagination = true) where TFilter : IFilter;
        Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(TFilter filter, bool hasPagination = true, params Expression<Func<TEntity, object>>[] includeProperties) where TFilter : IFilter;
        Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(TFilter filter, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy = null, bool hasPagination = true, params Expression<Func<TEntity, object>>[] includeProperties) where TFilter : IFilter;
        Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(TFilter filter, Expression<Func<TEntity, object>> orderBy = null, bool hasPagination = true, params Expression<Func<TEntity, object>>[] includeProperties) where TFilter : IFilter;
        Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(TFilter filter, Expression<Func<TEntity, bool>> predicate, IQueryable<TEntity> queryable = null, Expression<Func<TEntity, object>> orderBy = null, bool hasPagination = true, params Expression<Func<TEntity, object>>[] includeProperties) where TFilter : IFilter;
        Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        void Dispose();
    }
}
