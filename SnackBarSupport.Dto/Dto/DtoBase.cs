using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Dto
{
    public abstract class DtoBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
