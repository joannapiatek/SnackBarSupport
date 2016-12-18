using System.Web.Http;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.DAL
{
    public interface IGenericRepository<in T> where T : DtoBase
    {
        IHttpActionResult Get();
        IHttpActionResult Get(string id);
        IHttpActionResult Post([FromBody] T entity);
        IHttpActionResult Put(string id, [FromBody] T newEntity);
        IHttpActionResult Delete(string id);
    }
}
