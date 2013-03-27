#region Usings

using System;
using System.Threading.Tasks;
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

namespace LOB.Dao.Nhibernate
{
    public class SessionCreator : ISessionCreator
    {
        private const string MySqlDefaultConnectionString = @"Server=192.168.0.150;
                        Database=LOB;Uid=LOB;Pwd=LOBPASSWD;";
        private const string MsSqlDefaultConnectionString = @"Data Source=192.168.0.151;
                        Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";
        private readonly ILoggerFacade _logger;
        private object _orm;
        private PersistType _persistType;
        private SchemaExport _sqlSchema;

        [InjectionConstructor]
        public SessionCreator(ILoggerFacade logger)
            //MySQL
            : this(logger, PersistType.MySql, MySqlDefaultConnectionString)
        {
        }

        private SessionCreator(ILoggerFacade logger, PersistType persistIn, string connectionString)
        {
            ConnectionString = connectionString;
            _logger = logger;
            _persistType = persistIn;
        }

        public string ConnectionString { get; set; }
        
        public Object Orm
        {
            get
            {
                return _orm ?? (_orm = SessionCreatorFactory(_persistType).OpenSession());
            }
        }

        public event SessionCreatorEventHandler OnCreatingSession;
        public event SessionCreatorEventHandler OnSessionCreated;

        private ISessionFactory SessionCreatorFactory(PersistType persistIn)
        {
            if(OnCreatingSession != null) OnCreatingSession.Invoke(this, new SessionCreatorEventArgs(Strings.Dao_Connecting));
            Configuration cfg = null;
            ISessionFactory factory = null;
            switch (_persistType)
            {
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
            if (cfg != null)
                try
                {
                    factory = cfg.BuildSessionFactory();
                    if (OnSessionCreated != null) OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(Strings.Dao_ConnectionSucessful));
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message, Category.Exception, Priority.High);
                    if (OnSessionCreated != null) OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(ex.Message));
                }
            return factory;
        }

        private Configuration StoreInMySqlConfiguration()
        {
            return Mapping().Database(MySQLConfiguration.Standard
                                                        .ConnectionString(ConnectionString))
                            .BuildConfiguration();
        }

        private Configuration StoreInMsSqlConfiguration()
        {
            return Mapping().Database(MsSqlConfiguration.MsSql2008
                                                        .ConnectionString(ConnectionString))
                            .BuildConfiguration();
        }

        private Configuration StoreInMemoryConfiguration()
        {
            return Mapping().Database(SQLiteConfiguration.Standard
                                                         .InMemory())
                            .BuildConfiguration();
        }

        private Configuration StoreInFileConfiguration()
        {
            return Mapping().Database(SQLiteConfiguration.Standard
                                                         .UsingFile("local.db"))
                            .BuildConfiguration();
        }

        private FluentConfiguration Mapping()
        {
            return Fluently.Configure().Mappings(x => x.FluentMappings.AddFromAssemblyOf<SessionCreator>())
                //Disable to much logging
                           .Diagnostics(x => x.Enable(false))
                //Generate Tables
                           .ExposeConfiguration(SchemaCreator);
        }

        private void SchemaCreator(Configuration cfg)
        {
            try
            {
                if (_persistType == PersistType.Memory)
                {
                    _sqlSchema = new SchemaExport(cfg);
                    _sqlSchema.Create(false, true);
                    return;
                }
                _sqlSchema = new SchemaExport(cfg);
                _sqlSchema.Drop(false, true);
                _sqlSchema.Create(false, true);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message, Category.Exception, Priority.High);
            }
        }
    }
}