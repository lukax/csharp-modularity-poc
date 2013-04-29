#region Usings

using System.ComponentModel.Composition;
using FluentNHibernate;
using LOB.Dao.Contract;
using LOB.Dao.Contract.Exception;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate {
    [Export, Export(typeof(IOrmFactory)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class OrmFactory : IOrmFactory {
        [Import] private OrmFactoryConfiguration OrmFactoryConfiguration { get; set; }

        public object Orm {
            get {
                var a = OrmFactoryConfiguration.OrmFactory as ISessionFactory;
                var b = OrmFactoryConfiguration.OrmFactory as ISessionSource;
                try {
                    return a != null ? a.OpenSession() : b != null ? b.CreateSession() : new object();
                } catch(HibernateException) {
                    throw new DatabaseConnectionException();
                }
            }
        }
    }
}