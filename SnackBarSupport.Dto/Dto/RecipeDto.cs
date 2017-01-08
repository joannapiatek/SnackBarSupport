﻿using System.Collections.Generic;
using Models.ViewModels;

namespace Models.Dto
{
    public class RecipeDto : DtoBase
    {
        public string Name { get; set; }
        public ICollection<IngredientDto> Ingredients { get; set; }
        public double Price { get; set; }

        public RecipeDto() { }

        public RecipeDto(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Price = recipe.Price;
            Ingredients = recipe.IngredientsForRecipe;
        }

        public RecipeDto(RecipeSelect select)
        {
            Id = select.Id;
            Name = select.Name;
            Price = select.Price;
            Ingredients = select.Ingredients ?? new List<IngredientDto>();
        }
    }
}
