using System.Web.Http;
using SnackBarSupport.DAL;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.Controllers
{
    [RoutePrefix("api/saleshistory")]
    public class SalesHistoryController : GenericRepository<SaleHistory>
    {
    }
}
