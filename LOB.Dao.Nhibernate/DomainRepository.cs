#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using NHibernate;
using NHibernate.Linq;

#endregion

namespace LOB.Dao.Nhibernate
{
    public class DomainRepository : IRepository
    {
        [ImportingConstructor]
        public DomainRepository(IUnityOfWork unityOfWork)
        {
            Uow = unityOfWork;
        }

        public IUnityOfWork Uow { get; private set; }

        public T Save<T>(T entity) where T : BaseEntity
        {
            Uow.Save(entity);
            return entity;
        }

        public T Update<T>(T entity) where T : BaseEntity
        {
            Uow.Update(entity);
            return entity;
        }

        public T SaveOrUpdate<T>(T entity) where T : BaseEntity
        {
            Uow.SaveOrUpdate(entity);
            return entity;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            Uow.Delete(entity);
        }

        public T Get<T>(object primaryKey) where T : BaseEntity
        {
            return GetSession().Get<T>(primaryKey);
        }

        public bool Contains<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity
        {
            return GetSession().Query<T>().Contains(
                GetSession().Query<T>().FirstOrDefault(criteria));
        }

        public bool Contains<T>(T entity) where T : BaseEntity
        {
            return GetSession().Query<T>().Contains(
                GetSession().Query<T>().FirstOrDefault(x => x == entity));
        }

        public IQueryable<T> GetList<T>() where T : BaseEntity
        {
            return GetSession().Query<T>();
        }

        public IQueryable<T> GetList<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity
        {
            return GetSession().Query<T>().Where(criteria);
        }

        private ISession GetSession()
        {
            return (ISession) Uow.Orm;
        }

        public bool Contains<T>(int code) where T : BaseEntity
        {
            return GetSession().Query<T>().Contains(
                GetSession().Query<T>().FirstOrDefault(x => x.Code == code));
        }
    }
}