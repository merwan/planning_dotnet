using System;
using System.Data.SQLite;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MealPlanner.Core.ReadModel;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace MealPlanner.Tests
{
    /// <summary>
    /// Extrait ici : http://jasondentler.com/blog/2009/08/nhibernate-unit-testing-with-sqlite-in-memory-db/
    /// </summary>
    public class SQLiteDatabaseScope : IDisposable
    {
        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        public SQLiteDatabaseScope()
        {
            BuildConfiguration();
        }

        private SQLiteConnection m_Connection;
        private ISessionFactory m_SessionFactory;

        public ISessionFactory SessionFactory
        {
            get { return m_SessionFactory; }
        }

        private void BuildConfiguration()
        {
            m_SessionFactory = Fluently.Configure()
                    .Database(GetDBConfig())
                    .Mappings(GetMappings)
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
        }

        private IPersistenceConfigurer GetDBConfig()
        {
            return SQLiteConfiguration.Standard
                .ConnectionString(cs => cs.Is(CONNECTION_STRING));
        }

        private static void GetMappings(MappingConfiguration x)
        {
            x.AutoMappings
                .Add(AutoMap.AssemblyOf<IngredientDTO>());
        }

        private void BuildSchema(NHibernate.Cfg.Configuration cfg)
        {
            SchemaExport SE = new SchemaExport(cfg);
            SE.Execute(false, true, false, GetConnection(), Console.Out);
        }

        private SQLiteConnection GetConnection()
        {
            if (m_Connection == null)
            {
                m_Connection = new SQLiteConnection(CONNECTION_STRING);
                m_Connection.Open();
            }
            return m_Connection;
        }

        public ISession OpenSession()
        {
            return SessionFactory.OpenSession(GetConnection());
        }

        public IStatelessSession OpenStatelessSession()
        {
            return SessionFactory.OpenStatelessSession(GetConnection());
        }

        private bool disposedValue = false;
        // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: free other state (managed objects).
                    if (m_Connection != null) m_Connection.Close();
                    m_Connection = null;

                }
            }
            // TODO: free your own state (unmanaged objects).
            // TODO: set large fields to null.
            disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}