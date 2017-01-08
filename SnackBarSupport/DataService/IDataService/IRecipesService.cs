using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IRecipesService
    {
        Task<List<RecipeDto>> GetAllAsync();
        Task<RecipeDto> GetAsync(string id);
        Task AddAsync(RecipeDto recipe);
        Task DeleteAsync(string id);
        Task UpdateAsync(RecipeDto recipe);
    }
}
