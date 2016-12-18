using System.Web.Http;
using SnackBarSupport.DAL;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.Controllers
{
    [RoutePrefix("api/recipies")]
    public class RecipiesController : GenericRepository<Recipe>
    {
    }
}
