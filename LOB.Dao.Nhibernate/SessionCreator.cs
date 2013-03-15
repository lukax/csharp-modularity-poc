#region Usings

using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LOB.Dao.Interface;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Remotion.Linq.Utilities;

#endregion

namespace LOB.Dao.Nhibernate
{
    public class SessionCreator : ISessionCreator
    {
        private const String MySqlDefaultConnectionString = @"Server=192.168.0.150;
                        Database=LOB;Uid=LOB;Pwd=LOBPASSWD;";
        private const String MsSqlDefaultConnectionString = @"Data Source=192.168.0.151;
                        Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";
        public static readonly ISessionCreator Default = new SessionCreator();
        private String _connectionString;
        private Lazy<object> _lazyOrm;
        private object _orm;
        private PersistType _persistType;
        private SchemaExport _sqlSchema;

        //[InjectionConstructor]
        private SessionCreator()
            : this(PersistType.MsSql, null)
        {
        }

        private SessionCreator(PersistType persistIn, String connectionString)
        {
            if (connectionString != null)
                ConnectionString = connectionString;
            _persistType = persistIn;

            _lazyOrm = new Lazy<object>(() => SessionCreatorFactory(persistIn).OpenSession());
        }

        public String ConnectionString
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
            get { return _orm ?? _lazyOrm.Value; }
            private set { _orm = value; }
        }

        private ISessionFactory SessionCreatorFactory(PersistType persistIn)
        {
            _persistType = persistIn;
            Configuration cfg;
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
                    throw new ArgumentEmptyException("persistIn");
            }
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
                //Disable log
                           .Diagnostics(x => x.Enable(false))
                //Generate Tables
                           .ExposeConfiguration(SchemaCreator);
        }

        private void SchemaCreator(Configuration cfg)
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
            //_sqlSchema.Execute(false, true, true);
        }
    }
}