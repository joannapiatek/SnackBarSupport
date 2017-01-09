using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Models.Dto;
using SnackBarSupport.DataService;
using SnackBarSupport.DataService.IDataService;

namespace SnackBarSupport.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantsService _restaurantsService;
        private readonly IIngredientsService _ingredientsService;

        public RestaurantsController(IRestaurantsService restaurantsService, IIngredientsService ingredientsService)
        {
            _restaurantsService = restaurantsService;
            _ingredientsService = ingredientsService;
        }

        public RestaurantsController()
        {
            _restaurantsService = new RestaurantsService();
            _ingredientsService = new IngredientsService();
        }

        public async Task<ActionResult> Index()
        {
            var collection = await _restaurantsService.GetAllAsync();
            return View(collection);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RestaurantDto restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.IngredientsCountDictionary = await CreateIngredientsCountDictAsync();
                await _restaurantsService.AddAsync(restaurant);
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }       

        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var restaurant = await _restaurantsService.GetAsync(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }

            return View(restaurant);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var restaurant = await _restaurantsService.GetAsync(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }

            return View(restaurant);
        } 
        
        [HttpPost]    
        public async Task<ActionResult> Edit(RestaurantDto restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            if (!restaurant.IngredientsCountDictionary.Any())
            {
                var oldRestaurant = await _restaurantsService.GetAsync(restaurant.Id);
                restaurant.IngredientsCountDictionary = oldRestaurant.IngredientsCountDictionary;
            }

            await _restaurantsService.UpdateAsync(restaurant);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _restaurantsService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<Dictionary<IngredientDto, int>> CreateIngredientsCountDictAsync()
        {
            var dict = new Dictionary<IngredientDto, int>();
            var ingredients = await _ingredientsService.GetAllAsync();

            foreach (var ingredientDto in ingredients)
            {
                dict.Add(ingredientDto, 0);
            }

            return dict;
        }
    }
}