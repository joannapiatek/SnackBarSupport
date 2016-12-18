using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SnackBarSupport.Dto.Models
{
    public class Restaurant : DtoBase
    {
        public string Name { get; set; }

        public int StorehouseId { get; set; }
    }
}
