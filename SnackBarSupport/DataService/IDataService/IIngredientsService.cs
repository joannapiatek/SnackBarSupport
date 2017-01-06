using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IIngredientsService
    {
        Task<List<Ingredient>> GetAll();
        Task<Ingredient> Get(string id);
        Task Add(Ingredient ingredient);
        Task Delete(string id);
        Task Update(Ingredient ingredient);
    }
}
