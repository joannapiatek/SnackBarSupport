using System.Collections.Generic;
using Models.Dto;

namespace Models.ViewModels
{
    public class Resources
    {
        public RestaurantDto Restaurant { get; set; }
        public IList<IngredientDto> Ingredients { get; set; }
    }
}
