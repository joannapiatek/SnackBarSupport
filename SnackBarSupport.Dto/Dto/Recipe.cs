using System.Collections.Generic;

namespace Models.Dto
{
    public class Recipe : DtoBase
    {
        public string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public double Price { get; set; }      
    }
}
