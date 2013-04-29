using System;
using System.ComponentModel.Composition;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LOB.Dao.Contract;
using Microsoft.Practices.Prism.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace LOB.Dao.Nhibernate {
    [Export, Export(typeof(IOrmFactoryConfiguration)), PartCreationPolicy(CreationPolicy.Shared)]
    public class OrmFactoryConfiguration : IOrmFactoryConfiguration {
        private const string MySqlDefaultConnectionString = @"Server=192.168.0.150;Database=LOB;Uid=APP;Pwd=ANZW3YGtH7DSqtUG;";
        private const string MsSqlDefaultConnectionString = @"Data Source=192.168.0.151;Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";
        private string _connectionString;
        private object _orm;
        private readonly PersistType _persistType;
        private SchemaExport _sqlSchema;
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

        public bool IsNewDatabase { get; private set; }

        public object OrmFactory {
            get {
                try {
                    return _orm ?? (_orm = CreateSessionSource());
                } catch(NullReferenceException ex) {
                    Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                }
                return null;
            }
        }

        public OrmFactoryConfiguration()
            : this(PersistType.MySql) { }

        public OrmFactoryConfiguration(PersistType persistIn, string connectionString = null, bool isNewDatabase = false) {
            if(connectionString != null) ConnectionString = connectionString;
            _persistType = persistIn;
            IsNewDatabase = isNewDatabase;
        }

        public ISessionFactory CreateSessionFactory() {
            FluentConfiguration cfg;
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
                }
            return factory;
        }

        public ISessionSource CreateSessionSource() {
            FluentConfiguration cfg;
            ISessionSource sessionSource = null;
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
                    sessionSource = new SessionSource(cfg);
                } catch(Exception ex) {
                    Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                }
            return sessionSource;
        }

        private FluentConfiguration MySqlConfiguration() { return Mapping().Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString)); }
        private FluentConfiguration MsSqlConfiguration() { return Mapping().Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString)); }
        private FluentConfiguration InMemoryConfiguration() { return Mapping().Database(SQLiteConfiguration.Standard.InMemory()); }
        private FluentConfiguration FileConfiguration() { return Mapping().Database(SQLiteConfiguration.Standard.UsingFile("local.db")); }

        private FluentConfiguration Mapping() {
            return Fluently.Configure().Mappings(x => x.FluentMappings.AddFromAssemblyOf<OrmFactoryConfiguration>())
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
                if(IsNewDatabase) {
                    _sqlSchema.Drop(false, true);
                    _sqlSchema.Create(false, true);
                }
            } catch(HibernateException e) {
                Logger.Value.Log(e.Message, Category.Exception, Priority.High);
                throw;
            }
        }
        #region Implementation of IDisposable

        ~OrmFactoryConfiguration() { Dispose(false); }
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