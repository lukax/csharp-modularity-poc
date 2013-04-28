#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.Util.Contract;
using Microsoft.Practices.Prism.Logging;
using NHibernate;
using NHibernate.Linq;

#endregion

namespace LOB.Dao.Nhibernate {
    [Export(typeof(IRepository)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class Repository : IRepository, IPartImportsSatisfiedNotification {
        protected ISession Session { get; private set; }
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)] public IUnityOfWork Uow { get; private set; }
        [Import] protected Lazy<ILoggerFacade> LoggerFacade { get; private set; }
        [Import(ServiceName.NotificationService)] protected Lazy<Action<Notification>> NotificationSystem { get; private set; }
        public void OnImportsSatisfied() { Session = Uow.ORM.As<ISession>(); }

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
        public long Count<T>() where T : BaseEntity {
            try {
                return GetAll<T>().Count();
            } catch(ADOException ex) {
                LoggerFacade.Value.Log(ex.Message, Category.Exception, Priority.High);
                NotificationSystem.Value(new Notification(type: NotificationType.Error, message: Strings.Notification_RequisitionFailed,
                                                          detail: ex.Message));
                return 0;
            }
        }
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