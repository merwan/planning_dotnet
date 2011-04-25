using System;

namespace MealPlanner.Core.ReadModel
{
    public class IngredientDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public IngredientDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        protected IngredientDTO()
        {
        }
    }
}