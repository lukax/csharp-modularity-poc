#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using LOB.Dao.Contract;
using LOB.Dao.Contract.Exception.Database;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Logging;
using NHibernate;
using NHibernate.Linq;

#endregion

namespace LOB.Dao.Nhibernate {
    [Export(typeof(IRepository)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class Repository : IRepository {
        private ISession _ormAsSession;
        [Import]
        protected Lazy<ILoggerFacade> Logger { get; private set; }
        protected ISession ORM {
            get { return _ormAsSession ?? (_ormAsSession = Uow.Orm.As<ISession>()); }
        }
        [Import]
        public IUnityOfWork Uow { get; private set; }

        public T Get<T>(object id) where T : BaseEntity {
            try {
                return ORM.Get<T>(id);
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public T Load<T>(object id) where T : BaseEntity {
            try {
                return ORM.Load<T>(id);
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public T Save<T>(T entity) where T : BaseEntity {
            try {
                ORM.Save(entity);
                return entity;
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public T Update<T>(T entity) where T : BaseEntity {
            try {
                ORM.Update(entity);
                return entity;
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public T SaveOrUpdate<T>(T entity) where T : BaseEntity {
            try {
                ORM.SaveOrUpdate(entity);
                return entity;
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public T SaveOrUpdateCopy<T>(T entity) where T : BaseEntity {
            try {
                ORM.SaveOrUpdate(entity);
                return entity;
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public void Delete<T>(T entity) where T : BaseEntity {
            try {
                ORM.Delete(entity);
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public void DeleteAll<T>() where T : BaseEntity {
            try {
                ORM.Delete(string.Format("from {0}", typeof(T).Name));
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public bool Contains<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity {
            try {
                return ORM.Query<T>().Contains(ORM.Query<T>().FirstOrDefault(criteria));
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public long Count<T>() where T : BaseEntity {
            try {
                return GetAll<T>().Count();
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public long Count<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity {
            try {
                return GetAll(criteria).Count();
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public bool Contains<T>(T entity) where T : BaseEntity {
            try {
                return ORM.Query<T>().Contains(ORM.Query<T>().FirstOrDefault(x => x == entity));
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public IQueryable<T> GetAll<T>() where T : BaseEntity {
            try {
                return ORM.Query<T>();
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity {
            try {
                return ORM.Query<T>().Where(criteria);
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public T GetOne<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity {
            try {
                T temp = ORM.Query<T>().FirstOrDefault();
                return temp == default(T) ? null : temp;
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
        public bool Contains<T>(int code) where T : BaseEntity {
            try {
                return ORM.Query<T>().Contains(ORM.Query<T>().FirstOrDefault(x => x.Code == code));
            } catch(ADOException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                throw new DatabaseQueryException();
            }
        }
    }
}