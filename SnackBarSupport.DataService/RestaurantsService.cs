using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SnackBarSupport.DAL;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.DataService
{
    public class RestaurantsService
    {
        protected MongoDbContext MongoDbContext;

        protected RestaurantsService(MongoDbContext context)
        {
            MongoDbContext = context;
        }

        public List<Restaurant> GetAllrestaurants()
        {
            return MongoDbContext.GetCollection<Restaurant>()
                .Find(_ => true).ToList();
        }

        
    }
}
