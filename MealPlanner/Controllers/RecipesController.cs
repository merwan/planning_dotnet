using System.Web.Mvc;
using MealPlanner.Core.ReadModel;

namespace MealPlanner.Controllers
{
    public class RecipesController : SessionController
    {
        public ActionResult Index()
        {
            var recipes = Session.QueryOver<RecipeListDto>().List();
            return View(recipes);
        }
    }
}