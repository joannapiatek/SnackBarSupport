using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using MongoDB.Driver;
using SnackBarSupport.DAL;
using SnackBarSupport.Dto.Models;

namespace SnackBarSupport.Controllers
{
    [RoutePrefix("api/recipes")]
    public class RecipesController : ApiController
    {
        public DbContext Context { get; set; }

        public RecipesController() : this(new DbContext())
        { }

        public RecipesController(DbContext context)
        {
            Context = context;
        }

        [HttpGet, Route("")]
        [ResponseType((typeof(IEnumerable<Recipe>)))]
        public IHttpActionResult Get()
        {
            var collection = Context.Recipes.Find(_ => true).ToList();
            return Ok(collection);
        }

        [HttpGet, Route("{id}")]
        [ResponseType((typeof(IEnumerable<Recipe>)))]
        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var recipe = GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post([FromBody]Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Context.Recipes.InsertOne(recipe);
            return Ok(recipe);
        }

        [HttpPut, Route("{id}")]
        public IHttpActionResult Put(Recipe newRecipe)
        {
            var id = newRecipe.Id;
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var recipe = GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Name = newRecipe.Name;
            Context.Recipes.ReplaceOne(i => i.Id == id, recipe);

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IHttpActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var recipe = GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }

            Context.Recipes.DeleteOne(i => i.Id == id);

            return Ok();
        }

        private Recipe GetRecipe(string id)
        {
            return Context.Recipes
                .Find(i => i.Id == id)
                .FirstOrDefault();
        }
    }
}
