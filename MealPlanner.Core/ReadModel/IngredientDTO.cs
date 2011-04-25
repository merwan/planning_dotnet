using System;

namespace MealPlanner.Core.ReadModel
{
    public class IngredientDTO
    {
        public Guid Id;
        public string Name;

        public IngredientDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}