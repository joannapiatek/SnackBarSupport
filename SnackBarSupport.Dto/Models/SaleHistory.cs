using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SnackBarSupport.Dto.Models
{
    public class SaleHistory : DtoBase
    {
        public int RestaurantId { get; set; }

        public double Value { get; set; }

        public List<Recipe> Dishes { get; set; }

        public DateTime SaleDate { get; set; }
    }
}