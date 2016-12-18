using SnackBarSupport.DAL;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using MongoDB.Driver;
using SnackBarSupport.Dto.Models;

namespace SnackBarSupport.Controllers
{
    [RoutePrefix("api/ingredients")]
    public class IngredientsController : ApiController
    {
        public DbContext Context { get; set; }

        public IngredientsController() : this(new DbContext())
        { }

        public IngredientsController(DbContext context)
        {
            Context = context;
        }

        // GET: Ingredients
        [HttpGet, Route("")]
        [ResponseType((typeof(IEnumerable<Ingredient>)))]
        public IHttpActionResult Get()
        {
            var collection = Context.Ingredients.Find(_ => true).ToList();
            return Ok(collection);
        }

        [HttpGet, Route("{id}")]
        [ResponseType((typeof(IEnumerable<Ingredient>)))]
        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var ingredient = GetIngredient(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post([FromBody]Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Context.Ingredients.InsertOne(ingredient);
            return Ok(ingredient);
        }

        [HttpPut, Route("{id}")]
        public IHttpActionResult Put(Ingredient newIngredient)
        {
            var id = newIngredient.Id;
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var ingredient = GetIngredient(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.Name = newIngredient.Name;
            Context.Ingredients.ReplaceOne(i => i.Id == id, ingredient);

            return Ok();
        }       

        [HttpDelete, Route("{id}")]
        public IHttpActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var ingredient = GetIngredient(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            Context.Ingredients.DeleteOne(i => i.Id == id);

            return Ok();
        }

        private Ingredient GetIngredient(string id)
        {
            return Context.Ingredients
                .Find(i => i.Id == id)
                .FirstOrDefault();
        }
    }
}
