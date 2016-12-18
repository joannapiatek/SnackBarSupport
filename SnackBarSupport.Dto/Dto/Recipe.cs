using System.Collections.Generic;

namespace SnackBarSupport.Dto.Dto
{
    public class Recipe : DtoBase
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public double Price { get; set; }      
    }
}
