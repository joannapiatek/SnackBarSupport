using Models.Dto;
using SnackBarSupport.DataService.IDataService;
using SnackBarSupport.DAL;

namespace SnackBarSupport.DataService
{
    public class SalesHistoryService : GenericDataService<SaleHistoryDto>, ISaleHistoryService
    {
        protected SalesHistoryService(MongoDbContext context)
        {
            MongoDbContext = context;
        }

        public SalesHistoryService()
        {
            MongoDbContext = new MongoDbContext();
        }
    }
}