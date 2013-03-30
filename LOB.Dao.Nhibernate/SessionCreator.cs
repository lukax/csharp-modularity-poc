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
    public class SessionCreator : ISessionCreator {

        private const string MySqlDefaultConnectionString = @"Server=192.168.0.150;Database=LOB;Uid=LOB;Pwd=LOBPASSWD;";

        private const string MsSqlDefaultConnectionString =
            @"Data Source=192.168.0.151;Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";

        private readonly ILoggerFacade _logger;
        private string _connectionString;
        private object _orm;
        private readonly PersistType _persistType;
        private SchemaExport _sqlSchema;

        [InjectionConstructor] public SessionCreator(ILoggerFacade logger)
            : this(logger, PersistType.MySql) {}

        private SessionCreator(ILoggerFacade logger, PersistType persistIn, [AllowNull] string connectionString = null) {
            if(connectionString != null) this.ConnectionString = connectionString;
            this._logger = logger;
            this._persistType = persistIn;
        }

        [AllowNull] public string ConnectionString {
            get {
                if(this._connectionString != null) return this._connectionString;
                if(this._persistType == PersistType.MsSql) return MsSqlDefaultConnectionString;
                if(this._persistType == PersistType.MySql) return MySqlDefaultConnectionString;
                throw new NotSupportedException("PersistType");
            }
            set { this._connectionString = value; }
        }

        public Object ORM {
            get { return this._orm ?? (this._orm = this.SessionCreatorFactory(this._persistType).OpenSession()); }
        }

        public event SessionCreatorEventHandler OnCreatingSession;
        public event SessionCreatorEventHandler OnSessionCreated;

        private ISessionFactory SessionCreatorFactory(PersistType persistIn) {
            if(this.OnCreatingSession != null) this.OnCreatingSession.Invoke(this, new SessionCreatorEventArgs(Strings.Dao_Connecting));
            Configuration cfg = null;
            ISessionFactory factory = null;
            switch(this._persistType) {
                case PersistType.MySql:
                    cfg = this.StoreInMySqlConfiguration();
                    break;
                case PersistType.MsSql:
                    cfg = this.StoreInMsSqlConfiguration();
                    break;
                case PersistType.File:
                    cfg = this.StoreInFileConfiguration();
                    break;
                case PersistType.Memory:
                    cfg = this.StoreInMemoryConfiguration();
                    break;
                default:
                    throw new ArgumentException("PersistType");
            }
            if(cfg != null)
                try {
                    factory = cfg.BuildSessionFactory();
                    if(this.OnSessionCreated != null) this.OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(Strings.Dao_ConnectionSucessful));
                }
                catch(Exception ex) {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                    if(this.OnSessionCreated != null) this.OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(ex.Message));
                }
            return factory;
        }

        private Configuration StoreInMySqlConfiguration() {
            return
                this.Mapping()
                    .Database(MySQLConfiguration.Standard.ConnectionString(this.ConnectionString))
                    .BuildConfiguration();
        }

        private Configuration StoreInMsSqlConfiguration() {
            return
                this.Mapping()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(this.ConnectionString))
                    .BuildConfiguration();
        }

        private Configuration StoreInMemoryConfiguration() {
            return this.Mapping().Database(SQLiteConfiguration.Standard.InMemory()).BuildConfiguration();
        }

        private Configuration StoreInFileConfiguration() {
            return this.Mapping().Database(SQLiteConfiguration.Standard.UsingFile("local.db")).BuildConfiguration();
        }

        private FluentConfiguration Mapping() {
            return Fluently.Configure().Mappings(x => x.FluentMappings.AddFromAssemblyOf<SessionCreator>())
                //Disable to much logging
                           .Diagnostics(x => x.Enable(false))
                //Generate Tables
                           .ExposeConfiguration(this.SchemaCreator);
        }

        private void SchemaCreator(Configuration cfg) {
            try {
                this._sqlSchema = new SchemaExport(cfg);
                if(this._persistType == PersistType.Memory) {
                    this._sqlSchema.Create(false, true);
                    return;
                }
                this._sqlSchema.Drop(false, true);
                this._sqlSchema.Create(false, true);
            }
            catch(Exception e) {
                this._logger.Log(e.Message, Category.Exception, Priority.High);
            }
        }

    }
}