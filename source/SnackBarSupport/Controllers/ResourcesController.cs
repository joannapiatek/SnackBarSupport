using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Models.Dto;
using Models.ViewModels;
using SnackBarSupport.DataService;
using SnackBarSupport.DataService.IDataService;

namespace SnackBarSupport.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IRestaurantsService _restaurantsService;
        private readonly IIngredientsService _ingredientsService;

        public ResourcesController()
        {
            _restaurantsService = new RestaurantsService();
            _ingredientsService = new IngredientsService();
        }
        
        public ResourcesController(IRestaurantsService restaurantsService, IIngredientsService ingredientsService)
        {
            _restaurantsService = restaurantsService;
            _ingredientsService = ingredientsService;
        }

        public async Task<ActionResult> Index()
        {
            var collection = await _restaurantsService.GetAllAsync();
            return View(collection);
        }

        public async Task<ActionResult> Manage(RestaurantDto restaurant)
        {
            if (string.IsNullOrEmpty(restaurant?.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var restaurantDetails = await _restaurantsService.GetAsync(restaurant.Id);

            var ingredients = await _ingredientsService.GetAllAsync();

            var resources = new Resources()
            {
                Restaurant = restaurantDetails,
                Ingredients = ingredients
            };

            return View(resources);
        }

        public async Task<ActionResult> Increment(string ingredientId, string restaurantId)
        {
            var ingredientDto = new IngredientDto() {Id = ingredientId};
            var restaurant = await _restaurantsService.GetAsync(restaurantId);

            var dict = restaurant.IngredientsCountDictionary;
            if (dict.ContainsKey(ingredientDto))
            {
                dict[ingredientDto] += 1;
            }

            await _restaurantsService.UpdateAsync(restaurant);

            return RedirectToAction("Manage", restaurant);
        }
    }
}