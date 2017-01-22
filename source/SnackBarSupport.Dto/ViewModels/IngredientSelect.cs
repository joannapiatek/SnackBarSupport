using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.ViewModels
{
    public class IngredientSelect
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public IngredientSelect() {}

        public IngredientSelect(IngredientDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
        }
    }
}
