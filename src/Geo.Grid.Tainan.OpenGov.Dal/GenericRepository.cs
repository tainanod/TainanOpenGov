using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;

namespace Geo.Grid.Tainan.OpenGov.Dal
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private OpenGovContext context;

        public OpenGovContext Db { get { return context; } }

        public GenericRepository(IDbContextFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            context = factory.GetDbContext();
            if (context == null)
            {
                throw new ArgumentNullException("factory");
            }
        }

        //public DbSet<T> GetDbSet()
        //{
        //    return context.Set<T>();
        //}
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Set<T>().Add(entity);
            return SaveChanges();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="expression">The predicate.</param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().FirstOrDefault(expression);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsQueryable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}