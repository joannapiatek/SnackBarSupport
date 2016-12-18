using SnackBarSupport.DAL;
using System;
using System.Collections.Generic;
using System.Web.Http;
using MongoDB.Driver;
using SnackBarSupport.Dto.Models;

namespace SnackBarSupport.Controllers
{
    internal abstract class GenericRepository<T> 
        : ApiController, IGenericRepository<T> where T : DtoBase
    {
        protected MongoDbContext MongoDbContext;
        protected abstract string CollectionName { get; set; }

        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            var collection = GetAll();
            return Ok(collection);
        }

        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var entity = GetEntity(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            MongoDbContext
                .GetCollection<T>()
                .InsertOne(entity);
            return Ok(entity);
        }

        [HttpPut, Route("{id}")]
        public IHttpActionResult Put(T newEntity)
        {
            var id = newEntity.Id;
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            if (GetEntity(id) == null)
            {
                return NotFound();
            }

            MongoDbContext
                .GetCollection<T>()
                .ReplaceOne(i => i.Id == id, newEntity);

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            if (GetEntity(id) == null)
            {
                return NotFound();
            }

            MongoDbContext
                .GetCollection<T>()
                .DeleteOne(i => i.Id == id);

            return Ok();
        }

        protected T GetEntity(string id)
        {
            return MongoDbContext.GetCollection<T>()
                .Find(i => i.Id == id)
                .FirstOrDefault();
        }

        protected IList<T> GetAll()
        {
            return MongoDbContext
                .GetCollection<T>()
                .Find(_ => true).ToList();
        }
    }
}
