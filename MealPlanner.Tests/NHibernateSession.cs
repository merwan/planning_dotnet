using NHibernate;

namespace MealPlanner.Tests
{
    public static class NHibernateSession
    {
        private static readonly SQLiteDatabaseScope Scope = new SQLiteDatabaseScope();

        public static void Initialize()
        {
            CurrentSession = Scope.OpenSession();
            CurrentStatelessSession = Scope.OpenStatelessSession();
        }

        public static void Shutdown()
        {
            CurrentSession.Dispose();
            CurrentStatelessSession.Dispose();
        }

        public static ISession CurrentSession { get; private set; }
        public static IStatelessSession CurrentStatelessSession { get; private set; }
    }
}