using NHibernate;

namespace MealPlanner.Tests
{
    public abstract class GWTDatabase : GWT
    {
        protected ISession Session;
        protected IStatelessSession StatelessSession;

        protected abstract void LoadData();

        protected override void SetUp()
        {
            NHibernateSession.Initialize();
            NHibernateSession.CurrentSession.BeginTransaction();

            Session = NHibernateSession.CurrentSession;
            StatelessSession = NHibernateSession.CurrentStatelessSession;

            LoadData();
            Session.Flush();
        }

        protected sealed override void Post_action()
        {
            Session.Flush();
        }

        protected override void CleanUp()
        {
            NHibernateSession.CurrentSession.Transaction.Rollback();

            NHibernateSession.Shutdown();
        }
    }
}