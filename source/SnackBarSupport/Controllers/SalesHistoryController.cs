using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.Dto;
using Models.ViewModels;
using SnackBarSupport.DataService;
using SnackBarSupport.DataService.IDataService;

namespace SnackBarSupport.Controllers
{
    public class SalesHistoryController : Controller
    {
        private readonly IRestaurantsService _restaurantsService;
        private readonly ISaleHistoryService _salesHistorysService;
        private readonly IRecipesService _recipesService;

        public SalesHistoryController(ISaleHistoryService saleHistoryService, IRestaurantsService restaurantsService, IRecipesService recipesService)
        {
            _restaurantsService = restaurantsService;
            _salesHistorysService = saleHistoryService;
            _recipesService = recipesService;
        }

        public SalesHistoryController()
        {
            _restaurantsService = new RestaurantsService();
            _salesHistorysService = new SalesHistoryService();
            _recipesService = new RecipesService();
        }

        public async Task<ActionResult> Index()
        {
            var collectionDto = await _salesHistorysService.GetAllAsync();
            var collection = await ConvertToViewModel(collectionDto);

            return View(collection);
        }

        public async Task<ActionResult> Create()
        {
            var model = new SalesHistoryCreate();
            model.Restaurants = await _restaurantsService.GetAllAsync();
            var dishes = await _recipesService.GetAllAsync();
            model.AllDishes = ConvertToRecipesSelect(dishes);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SalesHistoryCreate model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", model);
            }

            model.Date = DateTime.Now;
            FillSelectedRecipes(model);
            var dto = new SaleHistoryDto(model);

            var restaurant = await _restaurantsService.GetAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return HttpNotFound();
            }

            var selectedDishes = model.AllDishes.Where(d => d.IsSelected).ToList();
            var dishesFromDb = await _recipesService.GetAllAsync();
            foreach (var dish in selectedDishes)
            {
                dish.Ingredients = dishesFromDb.Single(d => d.Id == dish.Id).Ingredients;
            }

            //if (!DishesCanBePrepared(
            //    selectedDishes, 
            //    restaurant.IngredientsCountDictionary))
            //{
            //    model.Message = Properties.Resources.LackOfIngredients;
            //    return View(model);
            //}

            try
            {
                SubtractIngredientsFromRestaurant(restaurant, selectedDishes);
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                model.Restaurants = await _restaurantsService.GetAllAsync();
                return View(model);
            }
            
            await _restaurantsService.UpdateAsync(restaurant);

            await _salesHistorysService.AddAsync(dto);
            return RedirectToAction("Index");
        }

        private async Task<IList<SaleHistoryList>> ConvertToViewModel(IList<SaleHistoryDto> collectionDto)
        {
            var salesHistory = new List<SaleHistoryList>();
            var restaurants = await _restaurantsService.GetAllAsync();

            foreach (var item in collectionDto)
            {
                var itemVm = new SaleHistoryList(item);
                itemVm.Restaurant = restaurants.SingleOrDefault(r => r.Id == item.RestaurantId);
                salesHistory.Add(itemVm);
            }

            return salesHistory;
        }

        private IList<RecipeSelect> ConvertToRecipesSelect(IList<RecipeDto> recipesDto)
        {
            var recipesSelect = new List<RecipeSelect>();
            foreach (var ing in recipesDto)
            {
                recipesSelect.Add(new RecipeSelect(ing));
            }
            return recipesSelect;
        }

        private void FillSelectedRecipes(SalesHistoryCreate history)
        {
            foreach (var dish in history.AllDishes.Where(d => d.IsSelected))
            {
                var dto = new RecipeDto(dish);
                history.DishesForSale.Add(dto.Name, dish.Count);
            }
        }

        //private bool DishesCanBePrepared(IList<RecipeSelect> dishes, IReadOnlyDictionary<IngredientDto, int> ingredientsCountDictionary)
        //{
        //    foreach (var dish in dishes)
        //    {
        //        DishCanBePrepared(dish, ingredientsCountDictionary);
        //    }

        //    if (dishes.Count(d => !d.CanBePrepared) > 0)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //How to do this?
        private bool DishCanBePrepared(RecipeSelect dish, IReadOnlyDictionary<IngredientDto, int> ingredientsCountDictionary, int amount)
        {
            dish.CanBePrepared = true;
            foreach (var ing in dish.Ingredients)
            {
                int value = 0;
                if (ingredientsCountDictionary.TryGetValue(ing, out value))
                {
                    if (value - amount >= 0) continue;
                    //niewiadomo, czy properta bedzie potrzebna
                    dish.CanBePrepared = false;
                    return false;
                }
                else
                {
                    //TODO: błąd!
                    //niewiadomo, czy properta bedzie potrzebna
                    dish.CanBePrepared = false;
                    return false;
                }
            }

            return true;
        }

        private void SubtractIngredientsFromRestaurant(RestaurantDto restaurant, IList<RecipeSelect> dishes)
        {
            var dict = restaurant.IngredientsCountDictionary;

            foreach (var dish in dishes)
            {
                if (!DishCanBePrepared(dish, dict, dish.Count))
                {
                    var message = string.Format(Properties.Resources.LackOfIngredients, dish.Name);
                    throw new Exception(message);
                }

                foreach (var ing in dish.Ingredients)
                {
                    SubtractIngredient(dict, ing, dish.Count);
                }
            }        
        }

        private void SubtractIngredient(Dictionary<IngredientDto, int> dict, IngredientDto ingredientDto, int amount)
        {
            if (dict.ContainsKey(ingredientDto))
            {
                dict[ingredientDto] -= amount;
            }
            else
            {
                var message = string.Format(Properties.Resources.IngredientNotFound, ingredientDto.Name);
                throw new Exception(message);
            }
        }
    }
}