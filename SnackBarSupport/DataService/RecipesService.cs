using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Models.Dto;
using SnackBarSupport.DataService.IDataService;
using SnackBarSupport.DAL;

namespace SnackBarSupport.DataService
{
    public class RecipesService : IRecipesService
    {
        protected MongoDbContext MongoDbContext;

        protected RecipesService(MongoDbContext context)
        {
            MongoDbContext = context;
        }

        public RecipesService()
        {
            MongoDbContext = new MongoDbContext();
        }

        public Task Add(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recipe>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}