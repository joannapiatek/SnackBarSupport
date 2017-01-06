using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;
using MongoDB.Driver;
using SnackBarSupport.DataService.IDataService;
using SnackBarSupport.DAL;

namespace SnackBarSupport.DataService
{
    public class RestaurantsService : IRestaurantsService
    {
        protected MongoDbContext MongoDbContext;

        protected RestaurantsService(MongoDbContext context)
        {
            MongoDbContext = context;
        }

        public RestaurantsService()
        {
            MongoDbContext = new MongoDbContext();
        }

        public async Task Add(Restaurant restaurant)
        {
            await MongoDbContext
                .GetCollection<Restaurant>()
                .InsertOneAsync(restaurant);
        }

        public async Task Delete(string id)
        {
            await MongoDbContext
                .GetCollection<Restaurant>()
                .DeleteOneAsync(i => i.Id == id);
        }

        public async Task Update(Restaurant restaurant)
        {
            await MongoDbContext
                .GetCollection<Restaurant>()
                .ReplaceOneAsync(i => i.Id == restaurant.Id, restaurant);
        }

        public async Task <List<Restaurant>> GetAll()
        {
            var task = MongoDbContext.GetCollection<Restaurant>()
                .FindAsync(_ => true);

            task.Wait();
            return task.Result.ToList();
        }

        public async Task<Restaurant> Get(string id)
        {
            return await MongoDbContext.GetCollection<Restaurant>()
                .Find(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
