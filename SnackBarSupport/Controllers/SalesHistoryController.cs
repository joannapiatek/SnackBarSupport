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
            if (ModelState.IsValid)
            {
                FillSelectedRecipes(model);
                var dto = new SaleHistoryDto(model);
                
                await _salesHistorysService.AddAsync(dto);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create", model);
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
    }
}