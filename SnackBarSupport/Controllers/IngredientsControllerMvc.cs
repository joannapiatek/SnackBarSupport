using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using SnackBarSupport.DAL;
using SnackBarSupport.Dto.Models;

namespace SnackBarSupport.Controllers
{
    public class IngredientsControllerMvc : Controller
    {
        public DbContext Context { get; set; }

        public IngredientsControllerMvc() : this(new DbContext())
        { }

        public IngredientsControllerMvc(DbContext context)
        {
            Context = context;
        }

        // GET: Ingredients
        public ActionResult Index()
        {
            var collection = View(Context.Ingredients.Find(_ => true).ToList());
            return collection;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                Context.Ingredients.InsertOne(ingredient);
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ingredient = GetIngredient(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            
            return View(ingredient);
        }

        [HttpPost]
        public ActionResult Edit(Ingredient newIngredient)
        {
            var id = newIngredient.Id;
            var ingredient = GetIngredient(newIngredient.Id);
            ingredient.Name = newIngredient.Name;
            Context.Ingredients.ReplaceOne(i => i.Id == id, ingredient);

            return RedirectToAction("Index");
        }       

        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ingredient = GetIngredient(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }

            return View(ingredient);
        }

        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ingredient = GetIngredient(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }

            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Context.Ingredients.DeleteOne(i => i.Id == id);

            return RedirectToAction("Index");
        }

        private Ingredient GetIngredient(string id)
        {
            return Context.Ingredients
                .Find(i => i.Id == id)
                .FirstOrDefault();
        }
    }
}