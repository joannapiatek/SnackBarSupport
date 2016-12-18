using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SnackBarSupport.Dto.Models
{
    public class Ingredient : DtoBase
    {
        public string Name { get; set; }
    }
}
