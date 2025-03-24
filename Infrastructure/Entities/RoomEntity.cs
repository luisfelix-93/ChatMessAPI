using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatMessAPI.Infrastructure.Entities
{
    public class RoomEntity
    {
        #region constructor
        public RoomEntity() { }
        #endregion
        #region attributes
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string RoomId { get; set; }
        [BsonElement("room")]
        public string Room { get; set; }
        [BsonElement("createdAt")]
        public string CreatedAt { get; set; }
        #endregion
    }
}
