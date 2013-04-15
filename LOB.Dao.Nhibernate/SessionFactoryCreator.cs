#region Usings

using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NullGuard;

#endregion

namespace LOB.Dao.Nhibernate {
    public class SessionFactoryCreator : ISessionFactoryCreator {
        private const string MySqlDefaultConnectionString = @"Server=192.168.0.150;Database=LOB;Uid=LOB;Pwd=LOBPASSWD;";

        private const string MsSqlDefaultConnectionString = @"Data Source=192.168.0.151;Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";

        private readonly ILoggerFacade _logger;
        private string _connectionString;
        private object _orm;
        private readonly PersistType _persistType;
        private SchemaExport _sqlSchema;
        [AllowNull]
        public string ConnectionString {
            get {
                if(_connectionString != null) return _connectionString;
                if(_persistType == PersistType.MsSql) return MsSqlDefaultConnectionString;
                if(_persistType == PersistType.MySql) return MySqlDefaultConnectionString;
                throw new NotSupportedException("PersistType");
            }
            set { _connectionString = value; }
        }
        [AllowNull]
        public object ORMFactory {
            get {
                try {
                    return _orm ?? (_orm = CreateSessionFactory());
                } catch(NullReferenceException e) {
                    _logger.Log(e.Message, Category.Exception, Priority.Low);
                    if(OnSessionCreated != null) OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(Strings.Notification_Dao_RequisitionFailed));
                }
                return null;
            }
        }

        public bool DropTables { get; set; }

        [InjectionConstructor]
        public SessionFactoryCreator(ILoggerFacade logger)
            : this(logger, PersistType.MySql) { }

        private SessionFactoryCreator(ILoggerFacade logger, PersistType persistIn, [AllowNull] string connectionString = null) {
            if(connectionString != null) ConnectionString = connectionString;
            _logger = logger;
            _persistType = persistIn;
        }

        public event SessionCreatorEventHandler OnCreatingSession;
        public event SessionCreatorEventHandler OnSessionCreated;

        private ISessionFactory CreateSessionFactory() {
            if(OnCreatingSession != null) OnCreatingSession.Invoke(this, new SessionCreatorEventArgs(Strings.Notification_Dao_Connecting));
            Configuration cfg;
            ISessionFactory factory = null;
            switch(_persistType) {
                case PersistType.MySql:
                    cfg = StoreInMySqlConfiguration();
                    break;
                case PersistType.MsSql:
                    cfg = StoreInMsSqlConfiguration();
                    break;
                case PersistType.File:
                    cfg = StoreInFileConfiguration();
                    break;
                case PersistType.Memory:
                    cfg = StoreInMemoryConfiguration();
                    break;
                default:
                    throw new ArgumentException("PersistType");
            }
            if(cfg != null)
                try {
                    factory = cfg.BuildSessionFactory();
                    if(OnSessionCreated != null) OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(Strings.Notification_Dao_ConnectionSucessful));
                } catch(Exception ex) {
                    _logger.Log(ex.Message, Category.Exception, Priority.High);
                    if(OnSessionCreated != null) OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(ex.Message));
                }
            return factory;
        }

        private Configuration StoreInMySqlConfiguration() { return Mapping().Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString)).BuildConfiguration(); }
        private Configuration StoreInMsSqlConfiguration() { return Mapping().Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString)).BuildConfiguration(); }
        private Configuration StoreInMemoryConfiguration() { return Mapping().Database(SQLiteConfiguration.Standard.InMemory()).BuildConfiguration(); }
        private Configuration StoreInFileConfiguration() { return Mapping().Database(SQLiteConfiguration.Standard.UsingFile("local.db")).BuildConfiguration(); }

        private FluentConfiguration Mapping() {
            return Fluently.Configure().Mappings(x => x.FluentMappings.AddFromAssemblyOf<SessionFactoryCreator>())
                //Disable to much logging
                           .Diagnostics(x => x.Enable(false))
                //Generate Tables
                           .ExposeConfiguration(SchemaCreator);
        }

        private void SchemaCreator(Configuration cfg) {
            try {
                _sqlSchema = new SchemaExport(cfg);
                if(_persistType == PersistType.Memory) {
                    _sqlSchema.Create(false, true);
                    return;
                }
                if(DropTables) {
                    _sqlSchema.Drop(false, true);
                    _sqlSchema.Create(false, true);
                }
            } catch(Exception e) {
                _logger.Log(e.Message, Category.Exception, Priority.High);
            }
        }
        #region Implementation of IDisposable

        ~SessionFactoryCreator() { Dispose(false); }
        public void Dispose() { Dispose(true); }
        public void Dispose(bool disposing) {
            var orm = _orm as ISessionFactory;
            if(orm == null) return;
            if(!disposing) orm.Close();
            else orm.Dispose();
        }

        #endregion
    }
}