#region Usings

using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using Microsoft.Practices.Prism.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

#endregion

namespace LOB.Dao.Nhibernate {
    [Export(typeof(ISessionFactoryCreator)), PartCreationPolicy(CreationPolicy.Shared)]
    public class SessionFactoryCreator : ISessionFactoryCreator {
        private const string MySqlDefaultConnectionString = @"Server=192.168.0.150;Database=LOB;Uid=LOB;Pwd=LOBPASSWD;";
        private const string MsSqlDefaultConnectionString = @"Data Source=192.168.0.151;Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";
        private string _connectionString;
        private object _orm;
        private readonly PersistType _persistType;
        private SchemaExport _sqlSchema;
        protected bool DropTables { get; set; }
        protected string ConnectionString {
            get {
                if(_connectionString != null) return _connectionString;
                if(_persistType == PersistType.MsSql) return MsSqlDefaultConnectionString;
                if(_persistType == PersistType.MySql) return MySqlDefaultConnectionString;
                throw new NotSupportedException("PersistType");
            }
            set { _connectionString = value; }
        }
        [Import] protected Lazy<ILoggerFacade> Logger { get; set; }
        public object ORMFactory {
            get {
                try {
                    return _orm ?? (_orm = CreateSessionFactory());
                } catch(NullReferenceException ex) {
                    Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                    if(OnError != null) OnError.Invoke(this, new SessionCreatorEventArgs(Strings.Notification_Dao_RequisitionFailed, ex.Message));
                }
                return null;
            }
        }
        public event SessionCreatorEventHandler OnError;

        public SessionFactoryCreator()
            : this(PersistType.MySql) { }

        private SessionFactoryCreator(PersistType persistIn, string connectionString = null, bool dropTables = false) {
            if(connectionString != null) ConnectionString = connectionString;
            _persistType = persistIn;
            DropTables = dropTables;
        }

        private ISessionFactory CreateSessionFactory() {
            Configuration cfg;
            ISessionFactory factory = null;
            switch(_persistType) {
                case PersistType.MySql:
                    cfg = MySqlConfiguration();
                    break;
                case PersistType.MsSql:
                    cfg = MsSqlConfiguration();
                    break;
                case PersistType.File:
                    cfg = FileConfiguration();
                    break;
                case PersistType.Memory:
                    cfg = InMemoryConfiguration();
                    break;
                default:
                    throw new ArgumentException("PersistType");
            }
            if(cfg != null)
                try {
                    factory = cfg.BuildSessionFactory();
                } catch(Exception ex) {
                    Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                    if(OnError != null) OnError.Invoke(this, new SessionCreatorEventArgs(Strings.Notification_Dao_Connecting_Failed, ex.Message));
                }
            return factory;
        }

        private Configuration MySqlConfiguration() { return Mapping().Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString)).BuildConfiguration(); }
        private Configuration MsSqlConfiguration() { return Mapping().Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString)).BuildConfiguration(); }
        private Configuration InMemoryConfiguration() { return Mapping().Database(SQLiteConfiguration.Standard.InMemory()).BuildConfiguration(); }
        private Configuration FileConfiguration() { return Mapping().Database(SQLiteConfiguration.Standard.UsingFile("local.db")).BuildConfiguration(); }

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
                Logger.Value.Log(e.Message, Category.Exception, Priority.High);
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