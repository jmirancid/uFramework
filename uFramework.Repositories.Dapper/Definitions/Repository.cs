using System;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using SQLinq;
using SQLinq.Dapper;
using uFramework.Interfaces.Repositories;

namespace uFramework.Repositories.Dapper.Definitions
{
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
        where TEntity : class, new()
        where TContext : IDbConnection, new()
    {

        private TContext _context;

        protected TContext Context
        {
            get
            {
                if (_context == null)
                    _context = new TContext();

                return _context;
            }
        }

        public virtual void Create(TEntity entity)
        {
            try
            {
                this.Context.Insert<TEntity>(entity);
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                this.Context.Update<TEntity>(entity);
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                this.Context.Delete<TEntity>(entity);
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual TEntity Get(object id)
        {
            try
            {
                return this.Context.Get<TEntity>(id);
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual TEntity GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var sql = (from e in new SQLinq<TEntity>() select e).Where(predicate).ToSQL();
                return this.Context.QueryFirst<TEntity>(sql.ToQuery(), sql.Parameters);
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual IQueryable<TEntity> All()
        {
            try
            {
                return this.Context.GetAll<TEntity>().AsQueryable();
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual IQueryable<TEntity> AllBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var sql = (from e in new SQLinq<TEntity>() select e).Where(predicate);
                return this.Context.Query(sql).AsQueryable();
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual IQueryable<TEntity> Filter(int skip, int take)
        {
            try
            {
                var sql = (from e in new SQLinq<TEntity>() select e).Skip(skip).Take(take);
                return this.Context.Query(sql).AsQueryable();
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual IQueryable<TEntity> FilterBy(int skip, int take, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var sql = (from e in new SQLinq<TEntity>() select e).Where(predicate).Skip(skip).Take(take);
                return this.Context.Query(sql).AsQueryable();
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual int Count()
        {
            try
            {
                var sql = (from e in new SQLinq<TEntity>() select e).Count().ToSQL();
                return (int)this.Context.ExecuteScalar(sql.ToQuery());
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }

        public virtual int CountBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var sql = (from e in new SQLinq<TEntity>() select e).Where(predicate).Count().ToSQL();
                return (int)this.Context.ExecuteScalar(sql.ToQuery(), sql.Parameters);
            }
            catch (Exception ex)
            {
                //throw RepositoryExceptionHandler.GetException(ex);
                throw ex;
            }
        }
    }
}

