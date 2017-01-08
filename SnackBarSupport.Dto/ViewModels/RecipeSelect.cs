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
    public class RecipeSelect
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<IngredientDto> Ingredients { get; set; }
        public double Price { get; set; }

        public bool IsSelected { get; set; }
        public int Count { get; set; }

        public RecipeSelect() {}

        public RecipeSelect(RecipeDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Ingredients = dto.Ingredients ?? new List<IngredientDto>();
            Price = dto.Price;
        }
    }
}
