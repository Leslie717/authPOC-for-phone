
using MongoDB.Bson.Serialization.Attributes;

namespace authAPIpoc.Models
{
    [BsonIgnoreExtraElements]
    public class AuditData: Entity
    {
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public DateTime createdDateTime { get; set; } = DateTime.Now;
        public DateTime modifiedDateTime { get; set; } = DateTime.Now;
    }
}
