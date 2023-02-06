using KingOrder.Database.Contexts;
using KingOrder.Database.Extensions;
using KingOrder.Domain.Entities;
using KingOrder.Domain.Shared.Interfaces;
using KingOrder.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace KingOrder.Database.Repositories
{
    public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region constants

        private const string _isDeleted = "IsDeleted";

        #endregion

        #region protected members

        protected KingOrderContext Db;

        #endregion

        #region private members

        private bool disposed = false;

        #endregion

        #region constructors

        public BaseRepository(KingOrderContext context)
        {
            Db = context;
        }

        #endregion

        #region public methods implementations

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            var entity = await Db.Set<TEntity>().AddAsync(obj);
            return entity.Entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Db.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity> GetFirstAsync() => await Db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int size) => await Db.Set<TEntity>().AsNoTracking().Skip((page - 1) * size).Take(size).ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = Db.Set<TEntity>().AsQueryable();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
                queryable = queryable.Include<TEntity, object>(includeProperty);

            return await queryable.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> orderBy = null)
        {
            var queryable = Db.Set<TEntity>().AsQueryable();
            queryable = queryable.OrderBy(orderBy);

            return await queryable.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id) => await Db.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetByIdAsync(Guid id, bool ignoreQueryFilter)
        {
            var queryable = Db.Set<TEntity>().AsNoTracking().AsQueryable();

            if (ignoreQueryFilter)
                queryable = queryable.IgnoreQueryFilters();

            return await queryable.FirstOrDefaultAsync(c => c.Guid == id);
        }

        public virtual async Task<TEntity> GetByIdAsync(
            Guid id,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = Db.Set<TEntity>().AsNoTracking().AsQueryable();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return await queryable.FirstOrDefaultAsync(c => c.Guid == id);
        }

        public async Task<TEntity> GetByIdAsync(long id) => await Db.Set<TEntity>().FindAsync(id);

        public async Task RemoveAsync(TEntity obj) => Db.Set<TEntity>().Remove(obj);

        public async Task SoftRemoveAsync(TEntity obj)
        {
            var property = obj.GetType().GetProperty(_isDeleted);

            if (property != null)
            {
                property.SetValue(obj, true);
                await UpdateAsync(obj);
            }
        }

        public async Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate)
            => Db.Set<TEntity>().Where(predicate);

        public async Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(TFilter filter, bool hasPagination = true) where TFilter : IFilter
            => await FindAllByAsync(filter: filter, predicate: null, orderBy: null, hasPagination: hasPagination, includeProperties: null);

        public async Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(
            TFilter filter,
            bool hasPagination = true,
            params Expression<Func<TEntity,
            object>>[] includeProperties) where TFilter : IFilter
            => await FindAllByAsync(filter: filter, predicate: null, orderBy: null, hasPagination: hasPagination, includeProperties: includeProperties);

        public async Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(
            TFilter filter,
            Expression<Func<TEntity, object>> orderBy = null,
            bool hasPagination = true,
            params Expression<Func<TEntity,
            object>>[] includeProperties) where TFilter : IFilter
                => await FindAllByAsync(filter: filter, predicate: null, queryable: null, orderBy: orderBy, hasPagination: hasPagination, includeProperties: includeProperties);

        public async Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(
            TFilter filter,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> orderBy = null,
            bool hasPagination = true,
            params Expression<Func<TEntity, object>>[] includeProperties) where TFilter : IFilter
                => await FindAllByAsync(filter: filter, predicate: predicate, queryable: null, orderBy: orderBy, hasPagination: hasPagination, includeProperties: includeProperties);

        public async Task<(IEnumerable<TEntity>, int)> FindAllByAsync<TFilter>(
            TFilter filter,
            Expression<Func<TEntity, bool>> predicate,
            IQueryable<TEntity> queryable = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool hasPagination = true,
            params Expression<Func<TEntity, object>>[] includeProperties) where TFilter : IFilter
        {
            if (predicate == null)
                predicate = PredicateExpressionExtensions.BuildPredicateExpressionByEntityAndFilter<TEntity, TFilter>(filter);
            else
            {
                var predicateFilter = PredicateExpressionExtensions.BuildPredicateExpressionByEntityAndFilter<TEntity, TFilter>(filter);
                if (predicateFilter != null)
                    predicate = predicate.And(predicateFilter);
            }

            if (queryable == null)
                queryable = Db.Set<TEntity>().AsNoTracking().AsQueryable();

            if (includeProperties != null)
                foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
                    queryable = queryable.Include(includeProperty);

            if (orderBy != null)
                queryable = queryable.OrderBy(orderBy);

            if (predicate != null)
                queryable = queryable.Where(predicate);

            if (hasPagination)
                return (await queryable.Skip((filter.GetPageNumber() - 1) * filter
                                        .GetPageSize())
                                        .Take(filter.GetPageSize())
                                        .ToListAsync(), queryable.Count());
            else
                return (await queryable.ToListAsync(), queryable.Count());
        }

        public async Task<IEnumerable<TEntity>> FindAllByAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = Db.Set<TEntity>().AsNoTracking().Where(predicate).AsQueryable();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return queryable;
        }

        public async Task<IEnumerable<TEntity>> FindAllByAsync(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> orderBy)
            => Db.Set<TEntity>().Where(predicate).OrderBy(orderBy);

        public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate)
            => await Db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<TEntity> FindByAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = Db.Set<TEntity>().AsNoTracking().Where(predicate).AsQueryable();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return queryable.FirstOrDefault();
        }

        public async Task UpdateAsync(TEntity obj)
            => Db.Entry(obj).State = EntityState.Modified;

        public async Task UpdateAsync(TEntity obj, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var existingObj = await Db.FindAsync<TEntity>(obj.Guid);

            if (includeProperties.Any())
            {
                foreach (var property in includeProperties)
                {
                    var propertyName = property.GetPropertyAccess().Name;
                    var dbItemsEntry = Db.Entry(existingObj).Collection(propertyName);
                    var accessor = dbItemsEntry.Metadata.GetCollectionAccessor();

                    await dbItemsEntry.LoadAsync();
                    var dbItemsMap = ((IEnumerable<BaseEntity>)dbItemsEntry.CurrentValue)
                        .ToDictionary(e => e.Guid);

                    var items = (IEnumerable<BaseEntity>)accessor.GetOrCreate(obj, false);

                    foreach (var item in items)
                    {
                        if (!dbItemsMap.TryGetValue(item.Guid, out var oldItem))
                            accessor.Add(existingObj, item, false);
                        else
                        {
                            Db.Entry(oldItem).CurrentValues.SetValues(item);
                            dbItemsMap.Remove(item.Guid);
                        }
                    }

                    foreach (var oldItem in dbItemsMap.Values)
                        accessor.Remove(existingObj, oldItem);
                }
            }

            Db.Entry(existingObj).CurrentValues.SetValues(obj);
            Db.Entry(existingObj).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region protected methods implementations

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    Db.Dispose();

            disposed = true;
        }

        #endregion
    }
}
