using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IRestaurantsService
    {
        Task<List<Restaurant>> GetAll();
        Task<Restaurant> Get(string id);
        Task Add(Restaurant restaurant);
        Task Delete(string id);
        Task Update(Restaurant restaurant);
    }
}
