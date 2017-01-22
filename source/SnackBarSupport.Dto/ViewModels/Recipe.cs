using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.ViewModels
{
    public class Recipe
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<IngredientDto> IngredientsForRecipe { get; set; }

        private IList<IngredientSelect> _allIngredients; 
        public IList<IngredientSelect> AllIngredients
        {
            get { return _allIngredients; }
            set
            {
                _allIngredients = value;

                foreach (var i in _allIngredients)
                {
                    var selected = IngredientsForRecipe
                        .SingleOrDefault(select => select.Id == i.Id);

                    if (selected == null) continue;
                    i.IsSelected = true;
                }
            }
        }

        public Recipe()
        {
            IngredientsForRecipe = new List<IngredientDto>();
        }

        public Recipe(RecipeDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Price = dto.Price;
            IngredientsForRecipe = dto.Ingredients ?? new List<IngredientDto>();
        }       
    }
}
