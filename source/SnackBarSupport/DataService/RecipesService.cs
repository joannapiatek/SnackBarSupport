using Models.Dto;
using SnackBarSupport.DataService.IDataService;
using SnackBarSupport.DAL;

namespace SnackBarSupport.DataService
{
    public class RecipesService : GenericDataService<RecipeDto>, IRecipesService
    {
        protected RecipesService(MongoDbContext context)
        {
            MongoDbContext = context;
        }

        public RecipesService()
        {
            MongoDbContext = new MongoDbContext();
        }      
    }
}