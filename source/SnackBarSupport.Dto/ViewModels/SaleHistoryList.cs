using System;
using System.Collections.Generic;
using Models.Dto;

namespace Models.ViewModels
{
    public class SaleHistoryList
    {        
        public string RestaurantId { get; set; }
        public double Value { get; set; }
        public IDictionary<string, int> Dishes { get; set; }
        public DateTime Date { get; set; }

        public RestaurantDto Restaurant { get; set; }

        public SaleHistoryList() {}

        public SaleHistoryList(SaleHistoryDto dto)
        {
            RestaurantId = dto.RestaurantId;
            Value = dto.Value;
            Dishes = dto.DishesCount;
            Date = dto.SaleDate;
        }
    }
}
