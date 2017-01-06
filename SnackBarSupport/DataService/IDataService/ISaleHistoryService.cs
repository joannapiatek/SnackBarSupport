using System.Collections.Generic;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface ISaleHistoryService
    {
        List<SaleHistory> GetAll();
        bool Add(SaleHistory saleHistory);
    }
}
