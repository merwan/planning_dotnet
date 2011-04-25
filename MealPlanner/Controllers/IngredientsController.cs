using System.Collections.Generic;
using MealPlanner.Core.ReadModel;
using NHibernate.Criterion;

namespace MealPlanner.Controllers
{
    public class IngredientsController : SessionController
    {
        public IEnumerable<IngredientDTO> SearchByName(string searchString)
        {
            var results =
                Session.QueryOver<IngredientDTO>()
                .WhereRestrictionOn(x => x.Name).IsInsensitiveLike(searchString, MatchMode.Start);

            return results.List();
        }
    }
}