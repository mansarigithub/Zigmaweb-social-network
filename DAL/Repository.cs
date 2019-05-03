using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ZigmaWeb.DAL
{
    public class Repository
    {
        static Repository _instance;
        ZigmaWebContext _context;

        private Repository()
        {
            this._context = new ZigmaWebContext();
        }

        public static Repository Instance
        {
            get
            {
                return (_instance ?? (_instance = new Repository()));
            }
        }

        public virtual TEntity Add<TEntity>(TEntity entity) where TEntity : class, new()
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public virtual TEntity Remove<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            return dbSet.Remove(entity);
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<TEntity> AsQueryable<TEntity>(string includeProperties = null) where TEntity : class, new()
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            
            return query;
        }

        public virtual TEntity Find<TEntity>(params object[] keyValues) where TEntity : class, new()
        {
            return _context.Set<TEntity>().Find(keyValues);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
