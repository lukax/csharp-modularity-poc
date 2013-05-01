#region Usings

using System;
using System.ComponentModel.Composition;
using FluentNHibernate;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Dao.Contract.Exception.Base;
using LOB.Dao.Contract.Exception.Database;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate {
    [Export, Export(typeof(IOrmFactory)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class OrmFactory : IOrmFactory {
        [Import] private OrmFactoryConfiguration OrmFactoryConfiguration { get; set; }

        public object Orm {
            get {
                var sessionSource = OrmFactoryConfiguration.CreateSessionSource();
                try {
                    return sessionSource.CreateSession();
                } catch(Exception ex) {
                    throw new GenericDatabaseException(Strings.Notification_Dao_InternalError, ex.Message, ex);
                }
            }
        }
    }
}