using System;
using System.Collections.Generic;

namespace SnackBarSupport.Dto.Dto
{
    public class SaleHistory : DtoBase
    {
        public int RestaurantId { get; set; }

        public double Value { get; set; }

        public List<Recipe> Dishes { get; set; }

        public DateTime SaleDate { get; set; }
    }
}