using System;
using FluentNHibernate.Automapping;
using MealPlanner.Core.ReadModel;

namespace MealPlanner.Tests
{
    public class MealPlannerAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(IngredientDTO).Namespace;
        }
    }
}