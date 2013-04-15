#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Interface {
    public interface IRepository {
        IUnityOfWork Uow { get; }
        T Get<T>(object id) where T : BaseEntity;
        T Load<T>(object id) where T : BaseEntity;
        T Save<T>(T entity) where T : BaseEntity;
        T Update<T>(T entity) where T : BaseEntity;
        T SaveOrUpdate<T>(T entity) where T : BaseEntity;
        T SaveOrUpdateCopy<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        void DeleteAll<T>() where T : BaseEntity;
        bool Contains<T>(T entity) where T : BaseEntity;
        bool Contains<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;
        long Count<T>() where T : BaseEntity;
        long Count<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;
        IQueryable<T> GetAll<T>() where T : BaseEntity;
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;
        T GetOne<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;
    }
}