using System.Collections.Generic;

namespace Models.Dto
{
    public class Restaurant : DtoBase
    {
        public string Name { get; set; }

        // Moze byc problem z dictionary
        public Dictionary<Ingredient, int> IngredientsCountDictionary { get; set; }
    }
}
