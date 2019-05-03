using System;
using System.Linq;
using System.Linq.Expressions;

namespace ZigmaWeb.Repository
{
    interface IGenericRepository<TEntity>
    {
        TEntity Add(TEntity entity);

        TEntity Remove(TEntity entity);

        TEntity Update(TEntity entity);

        IQueryable<TEntity> Read(bool noTracking = false);

        IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        TEntity FindById(params object[] keyValues);
        IQueryable<TEntity> AsQueryable();
        IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> path);
    }
}
