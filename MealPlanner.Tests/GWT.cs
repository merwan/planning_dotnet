using NUnit.Framework;

namespace MealPlanner.Tests
{
    /// <summary>
    /// Classe de base pour faire du Behavior Driven Development
    /// </summary>
    public abstract class GWT
    {
        [SetUp]
        public void MainSetup()
        {
            SetUp();
            Given();
            When();
            Post_action();
        }

        [TearDown]
        protected void MainTeardown()
        {
            CleanUp();
        }

        protected abstract void Given();
        protected abstract void When();
        protected virtual void SetUp() { }
        protected virtual void CleanUp() { }
        protected virtual void Post_action() { }
    }
}