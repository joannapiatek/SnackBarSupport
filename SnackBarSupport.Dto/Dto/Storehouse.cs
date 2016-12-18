using System.Collections.Generic;
using System.Web.Http;

namespace SnackBarSupport.Dto.Dto
{
    [RoutePrefix("api/storehouses")]
    public class Storehouse : DtoBase
    {
        public string Name { get; set; }

        // Moze byc problem z dictionary
        public Dictionary<Ingredient, int> IngredientsCountDictionary { get; set; }
    }
}
