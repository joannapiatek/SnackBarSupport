using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.Dto;
using Models.ViewModels;
using SnackBarSupport.DataService;
using SnackBarSupport.DataService.IDataService;

namespace SnackBarSupport.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesService _recipesService;
        private readonly IIngredientsService _ingredientsService;

        public RecipesController(IRecipesService recipesService, IIngredientsService ingredientsService)
        {
            _recipesService = recipesService;
            _ingredientsService = ingredientsService;
        }

        public RecipesController()
        {
            _recipesService = new RecipesService();
            _ingredientsService = new IngredientsService();
        }

        public async Task<ActionResult> Index()
        {
            var collection = (await _recipesService.GetAllAsync());
            return View(collection);
        }

        public async Task<ActionResult> Create()
        {
            var recipe = new Recipe();

            var ingredients = await _ingredientsService.GetAllAsync();
            recipe.AllIngredients = ConvertToIngredientsSelect(ingredients);

            return View(recipe);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {              
                FillSelectedIngredients(recipe);
                var recipeDto = new RecipeDto(recipe);

                await _recipesService.AddAsync(recipeDto);
                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipe = await _recipesService.GetAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipeDto = await _recipesService.GetAsync(id);
            if (recipeDto == null)
            {
                return HttpNotFound();
            }

            var recipe = new Recipe(recipeDto);
            var ingredients = await _ingredientsService.GetAllAsync();
            recipe.AllIngredients = ConvertToIngredientsSelect(ingredients);

            return View(recipe);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                FillSelectedIngredients(recipe);
                var recipeDto = new RecipeDto(recipe);

                await _recipesService.UpdateAsync(recipeDto);
                return RedirectToAction("Index");
            }

            return View(recipe);
        }
      
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _recipesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private IList<IngredientSelect> ConvertToIngredientsSelect(IList<IngredientDto> ingredientsDto)
        {
            var ingredientsSelect = new List<IngredientSelect>();
            foreach (var ing in ingredientsDto)
            {
                ingredientsSelect.Add(new IngredientSelect(ing));
            }
            return ingredientsSelect;
        }

        private void FillSelectedIngredients(Recipe recipe)
        {           
            foreach (var ing in recipe.AllIngredients.Where(i => i.IsSelected))
            {
                recipe.IngredientsForRecipe.Add(new IngredientDto(ing));
            }
        }
    }
}