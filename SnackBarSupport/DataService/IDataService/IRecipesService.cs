using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IRecipesService
    {
        Task<List<Recipe>> GetAll();
        Task<Recipe> Get(string id);
        Task Add(Recipe recipe);
        Task Delete(string id);
        Task Update(Recipe recipe);
    }
}
