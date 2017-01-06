using System.Collections.Generic;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface ISaleHistoryService
    {
        List<SaleHistory> GetAll();
        bool Add(SaleHistory saleHistory);
    }
}
