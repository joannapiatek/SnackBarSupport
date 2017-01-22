using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;

namespace SnackBarSupport.DataService.IDataService
{
    public interface ISaleHistoryService
    {
        Task<List<SaleHistoryDto>> GetAllAsync();
        Task<SaleHistoryDto> GetAsync(string id);
        Task AddAsync(SaleHistoryDto saleHistory);
        Task DeleteAsync(string id);
        Task UpdateAsync(SaleHistoryDto saleHistory);
    }
}
