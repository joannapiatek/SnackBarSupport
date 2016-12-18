using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SnackBarSupport.Dto.Models
{
    public class Storehouse : DtoBase
    {
        public string Name { get; set; }

        // Moze byc problem z dictionary
        public Dictionary<Ingredient, int> IngredientsCountDictionary { get; set; }
    }
}
