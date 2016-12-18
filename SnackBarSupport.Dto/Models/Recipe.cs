using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SnackBarSupport.Dto.Models
{
    public class Recipe : DtoBase
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public double Price { get; set; }      
    }
}
