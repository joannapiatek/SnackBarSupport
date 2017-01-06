using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IRecipesService
    {
        List<Recipe> GetAll();
        bool Add(Recipe recipe);
        bool Delete(Recipe recipe);
        bool Update(Recipe recipe);
    }
}
