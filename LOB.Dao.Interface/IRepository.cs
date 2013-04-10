#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Interface {
    public interface IRepository {

        IUnityOfWork Uow { get; }
        T Save<T>(T entity) where T : BaseEntity;
        T Update<T>(T entity) where T : BaseEntity;
        T SaveOrUpdate<T>(T entity) where T : BaseEntity;
        T Delete<T>(T entity) where T : BaseEntity;
        T Get<T>(object primaryKey) where T : BaseEntity;
        bool Contains<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;
        bool Contains<T>(T entity) where T : BaseEntity;
        IQueryable<T> GetList<T>() where T : BaseEntity;
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;
        Task<IQueryable<T>> GetListAsync<T>() where T : BaseEntity;
        Task<IQueryable<T>> GetListAsync<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;

    }
}