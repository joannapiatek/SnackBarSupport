using SnackBarSupport.Dto.Models;
using System.Web.Http;

namespace SnackBarSupport.Controllers
{
    internal interface IGenericRepository<in T> where T : DtoBase
    {
        IHttpActionResult Get();
        IHttpActionResult Get(string id);
        IHttpActionResult Post([FromBody] T entity);
        IHttpActionResult Put(T entity);
        IHttpActionResult Delete(string id);
    }
}
