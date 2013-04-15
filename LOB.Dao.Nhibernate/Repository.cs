#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Linq;

#endregion

namespace LOB.Dao.Nhibernate {
    public class Repository : IRepository {
        protected ISession Session {
            get { return Uow.As<UnityOfWork>().ORM.As<ISession>(); }
        }
        public IUnityOfWork Uow { get; private set; }

        [InjectionConstructor]
        public Repository(IUnityOfWork unityOfWork) { Uow = unityOfWork; }

        public T Get<T>(object id) where T : BaseEntity { return Session.Get<T>(id); }
        public T Load<T>(object id) where T : BaseEntity { return Session.Load<T>(id); }
        public T Save<T>(T entity) where T : BaseEntity {
            Session.Save(entity);
            return entity;
        }
        public T Update<T>(T entity) where T : BaseEntity {
            Session.Update(entity);
            return entity;
        }
        public T SaveOrUpdate<T>(T entity) where T : BaseEntity {
            Session.SaveOrUpdate(entity);
            return entity;
        }
        public T SaveOrUpdateCopy<T>(T entity) where T : BaseEntity {
            Session.SaveOrUpdate(entity);
            return entity;
        }
        public void Delete<T>(T entity) where T : BaseEntity { Session.Delete(entity); }
        public void DeleteAll<T>() where T : BaseEntity { Session.Delete(string.Format("from {0}", typeof(T).Name)); }
        public bool Contains<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity { return Session.Query<T>().Contains(Session.Query<T>().FirstOrDefault(criteria)); }
        public long Count<T>() where T : BaseEntity { return GetAll<T>().Count(); }
        public long Count<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity { return GetAll(criteria).Count(); }
        public bool Contains<T>(T entity) where T : BaseEntity { return Session.Query<T>().Contains(Session.Query<T>().FirstOrDefault(x => x == entity)); }
        public IQueryable<T> GetAll<T>() where T : BaseEntity { return Session.Query<T>(); }
        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity { return Session.Query<T>().Where(criteria); }
        public T GetOne<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity {
            var temp = Session.Query<T>().FirstOrDefault();
            return temp == default(T) ? null : temp;
        }
        public bool Contains<T>(int code) where T : BaseEntity { return Session.Query<T>().Contains(Session.Query<T>().FirstOrDefault(x => x.Code == code)); }
    }
}