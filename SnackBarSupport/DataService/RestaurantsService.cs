using Models.Dto;
using SnackBarSupport.DataService.IDataService;
using SnackBarSupport.DAL;

namespace SnackBarSupport.DataService
{
    public class RestaurantsService : GenericDataService<RestaurantDto>, IRestaurantsService
    {       
        protected RestaurantsService(MongoDbContext context)
        {
            MongoDbContext = context;
        }

        public RestaurantsService()
        {
            MongoDbContext = new MongoDbContext();
        }
    }
}
