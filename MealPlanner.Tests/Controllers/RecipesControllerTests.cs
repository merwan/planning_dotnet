using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MealPlanner.Controllers;
using MealPlanner.Core.ReadModel;
using NUnit.Framework;
using MvcContrib.TestHelper;

namespace MealPlanner.Tests.Controllers
{
    [TestFixture]
    public class RecipesControllerTests : GWTDatabase
    {
        private RecipesController _controller;
        private ActionResult _actionResult;

        protected override void LoadData()
        {
            var recipe = new RecipeListDto(Guid.NewGuid(), "Potatoes");
            Session.Save(recipe);
            recipe = new RecipeListDto(Guid.NewGuid(), "Hamburger");
            Session.Save(recipe);
        }

        protected override void Given()
        {
            _controller = new RecipesController { Session = Session };
        }

        protected override void When()
        {
            _actionResult = _controller.Index();
        }

        [Test]
        public void View_is_rendered()
        {
            _actionResult.AssertViewRendered();
        }

        [Test]
        public void Can_list_recipes()
        {
            var viewResult = _actionResult.AssertViewRendered();
            ((IEnumerable<RecipeListDto>)(viewResult.Model)).Count().ShouldEqual(2);
        }
    }
}
