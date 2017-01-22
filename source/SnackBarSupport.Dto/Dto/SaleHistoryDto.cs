using System;
using System.Collections.Generic;
using Models.ViewModels;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Models.Dto
{
    public class SaleHistoryDto : DtoBase
    {
        public string RestaurantId { get; set; }
        public double Value { get; set; }      
        public DateTime SaleDate { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public IDictionary<string, int> DishesCount { get; set; }

        public SaleHistoryDto() {}

        public SaleHistoryDto(SalesHistoryCreate create)
        {
            RestaurantId = create.RestaurantId;
            Value = create.Value;
            DishesCount = create.DishesForSale;
            SaleDate = create.Date;
        }
    }
}