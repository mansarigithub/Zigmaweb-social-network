using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ZigmaWeb.DataAccess;
using ZigmaWeb.DataAccess.Context;

namespace ZigmaWeb.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        public ZigmaWebContext _context;

        public GenericRepository(ZigmaWebContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public TEntity Remove(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.Entry(entity).State == EntityState.Detached) {
                dbSet.Attach(entity);
            }
            return dbSet.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> Read( bool noTracking = false)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (noTracking)
                query = query.AsNoTracking<TEntity>();
            return query;
        }

        public IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            var query = _context.Set<TEntity>().Where(predicate);
            if (noTracking)
                query = query.AsNoTracking<TEntity>();
            return query;
        }



        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            if (noTracking)
                return _context.Set<TEntity>().AsNoTracking<TEntity>().Single(predicate);
            return _context.Set<TEntity>().Single(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            if (noTracking)
                return _context.Set<TEntity>().AsNoTracking<TEntity>().SingleOrDefault(predicate);
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity FindById(params object[] keyValues)
        {
            return _context.Set<TEntity>().Find(keyValues);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            return _context.Set<TEntity>().Include<TEntity, TProperty>(path);
        }
    }
}
