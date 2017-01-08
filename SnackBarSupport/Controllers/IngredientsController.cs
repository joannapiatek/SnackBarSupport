using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Models.Dto;
using SnackBarSupport.DataService;
using SnackBarSupport.DataService.IDataService;

namespace SnackBarSupport.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(IIngredientsService service)
        {
            _ingredientsService = service;
        }

        public IngredientsController()
        {
            _ingredientsService = new IngredientsService();
        }

        public async Task<ActionResult> Index()
        {
            var collection = (await _ingredientsService.GetAllAsync());
            return View(collection);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(IngredientDto ingredient)
        {
            if (ModelState.IsValid)
            {
                await _ingredientsService.AddAsync(ingredient);
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ingredient = await _ingredientsService.GetAsync(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }

            return View(ingredient);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ingredient = await _ingredientsService.GetAsync(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }

            return View(ingredient);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(IngredientDto ingredient)
        {
            if (ModelState.IsValid)
            {
                await _ingredientsService.UpdateAsync(ingredient);
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _ingredientsService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}