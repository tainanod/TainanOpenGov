using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Geo.Grid.Tainan.OpenGov.Dal.Interface
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        //DbSet<T> GetDbSet();
        OpenGovContext Db { get; }

        int Create(T entity);

        int Update(T entity);

        int Delete(T entity);

        T Get(Expression<Func<T, bool>> expression);

        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        int SaveChanges();
    }
}