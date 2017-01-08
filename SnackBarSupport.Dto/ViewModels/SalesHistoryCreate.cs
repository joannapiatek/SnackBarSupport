using System;
using System.Collections.Generic;
using System.Linq;
using Models.Dto;

namespace Models.ViewModels
{
    public class SalesHistoryCreate
    {
        public double Value
        {
            get
            {
                double value = 0;
                foreach (var dish in AllDishes.Where(d => d.IsSelected))
                {
                    value += dish.Count*dish.Price;
                }
                return value;
            }
        }
        public IDictionary<string, int> DishesForSale { get; set; }
        public DateTime Date { get; set; }
        public string RestaurantId { get; set; }

        public IList<RestaurantDto> Restaurants { get; set; }

        private IList<RecipeSelect> _allDishes;
        public IList<RecipeSelect> AllDishes
        {
            get { return _allDishes; }
            set
            {
                _allDishes = value;

                foreach (var i in _allDishes)
                {
                    if (DishesForSale.ContainsKey(i.Name))
                    {
                        i.IsSelected = true;
                    }
                }
            }
        }

        public SalesHistoryCreate()
        {
            if (DishesForSale == null)
            {
                DishesForSale = new Dictionary<string, int>();
            }
        }
    }
}
