using System;

namespace MealPlanner.Core.ReadModel
{
    public class RecipeListDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public RecipeListDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        protected RecipeListDto()
        {
        }
    }
}