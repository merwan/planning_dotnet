using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Cfg;

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
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sessionController = filterContext.Controller as SessionController;
            if (sessionController == null)
            {
                return;
            }

            var session = sessionController.Session;
            if (session == null)
            {
                return;
            }

            session.Dispose();
        }
    }
}