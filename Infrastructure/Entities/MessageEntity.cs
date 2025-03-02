using MongoDB.Bson.Serialization.Attributes;


namespace ChatMessAPI.Infrastructure.Entities
{
    public class MessageEntity
    {
        #region constructor
        public MessageEntity() { }
        #endregion
        #region attributes
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ChatId { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("room")]
        public string Room { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("time")]
        public string Time { get; set; }
        #endregion
    }
}
