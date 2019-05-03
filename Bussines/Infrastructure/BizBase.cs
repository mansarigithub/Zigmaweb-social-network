using System;
using System.Linq;
using System.Linq.Expressions;
using ZigmaWeb.UnitOfWork;

namespace ZigmaWeb.Bussines.Infrastructure
{
    public abstract class BizBase<TEntity> where TEntity: class, new()
    {
        protected ICoreUnitOfWork UnitOfWork { get; set; }

        protected BizBase(ICoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #region Read
        public TEntity Find(params object[] keyValues)
        {
            return UnitOfWork.GetRepository<TEntity>().FindById(keyValues);
        }

        public TEntity ReadSingle(System.Linq.Expressions.Expression<Func<TEntity,bool>> predicate, bool noTracking = false)
        {
            return UnitOfWork.GetRepository<TEntity>().Single(predicate, noTracking);
        }

        public TEntity ReadSingleOrDefault(Expression<Func<TEntity,bool>> predicate, bool noTracking = false)
        {
            return UnitOfWork.GetRepository<TEntity>().SingleOrDefault(predicate, noTracking);
        }

        public IQueryable<TEntity> Read(bool noTracking = false)
        {
            return UnitOfWork.GetRepository<TEntity>().Read(noTracking);
        }

        public IQueryable<TEntity> Read(Expression<Func<TEntity,bool>> predicate, bool noTracking = false)
        {
            return UnitOfWork.GetRepository<TEntity>().Read(predicate, noTracking);
        }

        #endregion

        #region Any
        public bool Any(System.Linq.Expressions.Expression<Func<TEntity,bool>> predicate)
        {
            return UnitOfWork.GetRepository<TEntity>().Any(predicate);
        }
        #endregion

        #region Insert
        public TEntity Add(TEntity entity)
        {
            return UnitOfWork.GetRepository<TEntity>().Add(entity);
        }
        #endregion

        #region Update
        public TEntity Update(TEntity entity)
        {
            return UnitOfWork.GetRepository<TEntity>().Update(entity);
        }
        #endregion

        #region Remove
        public TEntity Remove(TEntity entity)
        {
            return UnitOfWork.GetRepository<TEntity>().Remove(entity);
        }
        #endregion

        #region Include
        public IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            return UnitOfWork.GetRepository<TEntity>().Include(path);
        }
        #endregion
    }
}
