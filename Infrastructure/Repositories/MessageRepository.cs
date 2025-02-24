using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatMessAPI.Infrastructure.Repositories
{
    public class MessageRepository
    {
        #region attributes
        private readonly DatabaseHelper _databaseHelper;
        #endregion
        #region constructor
        public MessageRepository(IOptions<DatabaseHelper> databaseHelper)
        {
            _databaseHelper = databaseHelper.Value;
        }
        #endregion
        #region methods
        private IMongoCollection<MessageEntity> GetMessageCollection()
        {
            var mongoClient = new MongoClient(_databaseHelper.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_databaseHelper.DatabaseName);
            var messageCollection = mongoDatabase.GetCollection<MessageEntity>(_databaseHelper.MessageCollection);

            return messageCollection;
        }

        public async Task InsertMessageAsync(Message pMessage)
        {
            var messageCollection = GetMessageCollection();
            var messageEntity = new MessageEntity
            {
                ChatId = ObjectId.GenerateNewId().ToString(),
                Username = pMessage.Username,
                Room = pMessage.Room,
                Text = pMessage.Text,
                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            await messageCollection.InsertOneAsync(messageEntity);
        }

        public async Task<MessageEntity[]> GetMessageList(string pRoom)
        {
            var messageCollection = GetMessageCollection();
            var filter = Builders<MessageEntity>.Filter.Eq("room", pRoom);
            var messageList = await messageCollection.Find(filter).ToListAsync();
            return messageList.ToArray();
        }

        public async Task  DeleteMessageAsync(string pChatId)
        {
            var messageCollection = GetMessageCollection();
            var filter = Builders<MessageEntity>.Filter.Eq("chatId", pChatId);
            await messageCollection.DeleteOneAsync(filter);
        }

        public async Task<MessageEntity> UpdateMessageUpdate(string pChatId, Message pMessage)
        {
            var messageCollection = GetMessageCollection();
            var filter = Builders<MessageEntity>.Filter.Eq("chatId", pChatId);
            var update = Builders<MessageEntity>.Update
                .Set("username", pMessage.Username)
                .Set("room", pMessage.Room)
                .Set("text", pMessage.Text)
                .Set("time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var options = new FindOneAndUpdateOptions<MessageEntity>()
            {
                ReturnDocument = ReturnDocument.After
            };
            var updatedMessage = await messageCollection.FindOneAndUpdateAsync(filter, update, options);
            return updatedMessage;
        }
        #endregion
    }
}
