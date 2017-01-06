using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.Dto;
using SnackBarSupport.DataService;
using SnackBarSupport.DataService.IDataService;

namespace SnackBarSupport.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesService _recipesService;

        public RecipesController(IRecipesService service)
        {
            _recipesService = service;
        }

        public RecipesController()
        {
            _recipesService = new RecipesService();
        }

        public async Task<ActionResult> Index()
        {
            var collection = (await _recipesService.GetAll());
            return View(collection);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                await _recipesService.Add(recipe);
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
            var recipe = await _recipesService.Get(id);
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

            var recipe = await _recipesService.Get(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                await _recipesService.Update(recipe);
                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _recipesService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}