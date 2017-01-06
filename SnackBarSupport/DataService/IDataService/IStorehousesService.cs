using System.Collections.Generic;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface IStorehousesService
    {
        List<Storehouse> GetAll();
        bool Add(Storehouse storehouse);
        bool Delete(Storehouse storehouse);
        bool Update(Storehouse storehouse);
    }
}
