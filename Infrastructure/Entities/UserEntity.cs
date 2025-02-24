using MongoDB.Bson.Serialization.Attributes;

namespace ChatMessAPI.Infrastructure.Entities
{
    public class UserEntity
    {
        #region constructor
        public UserEntity() { }
        #endregion
        #region attributes
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("socketId")]
        public string SocketId { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("room")]
        public string Room { get; set; }

        [BsonElement("joinedAt")]
        public DateTime JoinedAt { get; set; }
        #endregion
    }
}
