using System.Configuration;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace MealPlanner.Controllers
{
    public class NHibernateActionFilter : ActionFilterAttribute
    {
        private static readonly ISessionFactory SessionFactory = BuildSessionFactory();

        public static ISession CurrentSession
        {
            get { return HttpContext.Current.Items["NHibernateSession"] as ISession; }
            set { HttpContext.Current.Items["NHibernateSession"] = value; }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            return new Configuration()
                .Configure()
                .SetProperty("connection.connection_string", ConfigurationManager.ConnectionStrings["MailPlanner"].ToString())
                .BuildSessionFactory();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionController = filterContext.Controller as SessionController;
            if (sessionController == null)
            {
                return;
            }

            sessionController.Session = SessionFactory.OpenSession();
            sessionController.Session.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sessionController = filterContext.Controller as SessionController;
            if (sessionController == null)
            {
                return;
            }

            using (var session = sessionController.Session)
            {
                if (session == null)
                {
                    return;
                }

                if (!session.Transaction.IsActive)
                {
                    return;
                }

                if (filterContext.Exception != null)
                {
                    session.Transaction.Rollback();
                }
                else
                {
                    session.Transaction.Commit();
                }
            }
        }
    }
}