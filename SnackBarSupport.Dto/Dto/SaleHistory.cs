using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class SaleHistory : DtoBase
    {
        public int RestaurantId { get; set; }

        public double Value { get; set; }

        public ICollection<Recipe> Dishes { get; set; }

        public DateTime SaleDate { get; set; }
    }
}