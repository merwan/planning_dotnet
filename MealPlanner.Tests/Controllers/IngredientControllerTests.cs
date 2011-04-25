using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using MealPlanner.Controllers;
using MealPlanner.Core.ReadModel;
using NUnit.Framework;

namespace MealPlanner.Tests.Controllers
{
    [TestFixture]
    public class IngredientControllerTests : GWTDatabase
    {
        private IngredientController _controller;
        private IEnumerable<IngredientDTO> _searchResults;

        protected override void LoadData()
        {
            var ingredient = new IngredientDTO(Guid.NewGuid(), "Carrot");
            Session.Save(ingredient);
            ingredient = new IngredientDTO(Guid.NewGuid(), "Tomatoe");
            Session.Save(ingredient);
            ingredient = new IngredientDTO(Guid.NewGuid(), "Stomach");
            Session.Save(ingredient);
        }

        protected override void Given()
        {
            _controller = new IngredientController { Session = Session };
        }

        protected override void When()
        {
            _searchResults = _controller.SearchByName("toma");
        }

        [Test]
        public void Can_search_ingredients_by_name()
        {
            Assert.That(_searchResults.Count(), Is.EqualTo(1));
        }
    }
}