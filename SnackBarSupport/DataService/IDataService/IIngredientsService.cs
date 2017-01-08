using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IIngredientsService
    {
        Task<List<IngredientDto>> GetAllAsync();
        Task<IngredientDto> GetAsync(string id);
        Task AddAsync(IngredientDto ingredient);
        Task DeleteAsync(string id);
        Task UpdateAsync(IngredientDto ingredient);
    }
}
