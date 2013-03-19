#region Usings

using System;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LOB.Dao.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

#endregion

namespace LOB.Dao.Nhibernate
{
    public class SessionCreator : ISessionCreator
    {
        private const string MySqlDefaultConnectionString = @"Server=192.168.0.150;
                        Database=LOB;Uid=LOB;Pwd=LOBPASSWD;";
        private const string MsSqlDefaultConnectionString = @"Data Source=192.168.0.151;
                        Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";
        private readonly IServiceLocator _container;
        private readonly ILoggerFacade _logger;
        private string _connectionString;
        private object _orm;
        private PersistType _persistType;
        private SchemaExport _sqlSchema;

        [InjectionConstructor]
        public SessionCreator(ILoggerFacade logger)
            : this(logger, PersistType.MySql, null)
        {
        }

        public SessionCreator(ILoggerFacade logger, PersistType persistIn, string connectionString)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            if (connectionString != null) ConnectionString = connectionString;
            _logger = logger;
            _persistType = persistIn;
        }

        public string ConnectionString
        {
            get
            {
                if (_persistType == PersistType.MySql)
                    return _connectionString ?? MySqlDefaultConnectionString;
                if (_persistType == PersistType.MsSql)
                    return _connectionString ?? MsSqlDefaultConnectionString;
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        public Object Orm
        {
            get
            {
                BuildOrm();
                return _orm;
            }
            private set { _orm = value; }
        }

        public event SessionCreatorEventHandler OnCreatingSession;
        public event SessionCreatorEventHandler OnSessionCreated;

        private async void BuildOrm()
        {
            //TODO: Translation support
            OnCreatingSession.Invoke(this, new SessionCreatorEventArgs("Connecting to the database..."));
            Configuration cfg = null;
            await Task.Run(() =>
                {
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
                });
            if (cfg != null)
                try
                {
                    Orm = cfg.BuildSessionFactory().OpenSession();
                    OnSessionCreated.Invoke(this, new SessionCreatorEventArgs("Connection Successful!"));
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message, Category.Exception, Priority.High);
                    OnSessionCreated.Invoke(this, new SessionCreatorEventArgs(ex.Message));
                }
        }

        private ISessionFactory SessionCreatorFactory(PersistType persistIn)
        {
            OnCreatingSession.Invoke(this, new SessionCreatorEventArgs("Connecting to the Database..."));
            _persistType = persistIn;
            Configuration cfg = null;
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
            OnSessionCreated.Invoke(this, new SessionCreatorEventArgs("Connection Successful!"));
            return cfg.BuildSessionFactory();
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
                //Disable logging
                           .Diagnostics(x => x.Enable(false))
                //Generate Tables
                           .ExposeConfiguration(SchemaCreator);
        }

        private void SchemaCreator(Configuration cfg)
        {
            if (cfg == null) throw new ArgumentNullException("cfg");
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

        //private event SessionCreatorEventHandler callInMainThread;
    }
}