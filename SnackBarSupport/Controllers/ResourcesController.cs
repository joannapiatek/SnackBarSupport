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
        private IRestaurantsService _restaurantsService;
        private IRestaurantsService RestaurantsService
        {
            get { return _restaurantsService ?? (_restaurantsService = new RestaurantsService()); }
        }

        private IIngredientsService _ingredientsService;
        private IIngredientsService IngredientsService
        {
            get { return _ingredientsService ?? (_ingredientsService = new IngredientsService()); }
        }

        public async Task<ActionResult> Index()
        {
            var collection = await RestaurantsService.GetAllAsync();
            return View(collection);
        }

        public async Task<ActionResult> Manage(RestaurantDto restaurant)
        {
            if (string.IsNullOrEmpty(restaurant?.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var restaurantDetails = await RestaurantsService.GetAsync(restaurant.Id);

            var ingredients = await IngredientsService.GetAllAsync();

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
            var restaurant = await RestaurantsService.GetAsync(restaurantId);

            var dict = restaurant.IngredientsCountDictionary;
            if (dict.ContainsKey(ingredientDto))
            {
                dict[ingredientDto] += 1;
            }

            await RestaurantsService.UpdateAsync(restaurant);

            return RedirectToAction("Manage", restaurant);
        }
    }
}