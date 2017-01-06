﻿using System.Net;
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

        public RestaurantsController(IRestaurantsService service)
        {
            _restaurantsService = service;
        }

        public RestaurantsController()
        {
            _restaurantsService = new RestaurantsService();
        }

        public async Task<ActionResult> Index()
        {
            var collection = await _restaurantsService.GetAll();
            return View(collection);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                await _restaurantsService.Add(restaurant);
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
            var restaurant = await _restaurantsService.Get(id);
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

            var restaurant = await _restaurantsService.Get(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }

            return View(restaurant);
        } 
        
        [HttpPost]    
        public async Task<ActionResult> Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                await _restaurantsService.Update(restaurant);
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id == 0.ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _restaurantsService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}