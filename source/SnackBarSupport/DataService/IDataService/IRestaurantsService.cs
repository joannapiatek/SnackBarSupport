using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IRestaurantsService
    {
        Task<List<RestaurantDto>> GetAllAsync();
        Task<RestaurantDto> GetAsync(string id);
        Task AddAsync(RestaurantDto restaurant);
        Task DeleteAsync(string id);
        Task UpdateAsync(RestaurantDto restaurant);
    }
}
