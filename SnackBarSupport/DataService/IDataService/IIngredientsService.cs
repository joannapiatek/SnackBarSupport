using System.Collections.Generic;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IIngredientsService
    {
        List<Ingredient> GetAll();
        bool Add(Ingredient ingredients);
        bool Delete(Ingredient ingredients);
        bool Update(Ingredient ingredients);
    }
}
