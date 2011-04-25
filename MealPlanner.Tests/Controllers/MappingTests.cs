using FluentNHibernate.Testing;
using MealPlanner.Core.ReadModel;
using NUnit.Framework;

namespace MealPlanner.Tests.Controllers
{
    [TestFixture]
    public class MappingTests : GWTDatabase
    {
        protected override void Given()
        {
        }

        protected override void When()
        {

        }

        [Test]
        public void Can_map_IngredientDTO()
        {
            new PersistenceSpecification<IngredientDTO>(Session)
                .CheckProperty(x => x.Name, "20010401 184350")
                .VerifyTheMappings();
        }

        protected override void LoadData()
        {
        }
    }
}