using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string _id { get; set; }
        public string name { get; set; }
    }
}