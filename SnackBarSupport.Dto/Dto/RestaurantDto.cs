using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Models.Dto
{
    public class RestaurantDto : DtoBase
    {
        public RestaurantDto()
        {
            IngredientsCountDictionary = new Dictionary<IngredientDto, int>();
        }
        public string Name { get; set; }

        // Moze byc problem z dictionary
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<IngredientDto, int> IngredientsCountDictionary { get; set; }
    }
}
