using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace authAPIpoc.Models
{
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Identity { get; set; }
        public bool isPurged { get; set; } = false;
    }
}
