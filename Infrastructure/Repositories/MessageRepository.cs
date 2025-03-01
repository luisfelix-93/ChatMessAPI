using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Helpers;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatMessAPI.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
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
        /*
         * Método para obter a coleção de mensagens
         */
        private IMongoCollection<MessageEntity> GetMessageCollection()
        {
            var mongoClient = new MongoClient(_databaseHelper.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_databaseHelper.DatabaseName);
            var messageCollection = mongoDatabase.GetCollection<MessageEntity>(_databaseHelper.MessageCollection);

            return messageCollection;
        }
        /*
         * Método para inserir uma mensagem no banco de dados
         * params: {Message pMessage}
         * return: {Task}
         */
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
        /*
         * Método para obter a lista de mensagens por sala
         * params: {string pRoom}
         * return: {Task<List<MessageEntity>>}
         */
        public async Task<List<MessageEntity>> GetMessageList(string pRoom)
        {
            var messageCollection = GetMessageCollection();
            var filter = Builders<MessageEntity>.Filter.Eq("room", pRoom);
            var messageList = await messageCollection.Find(filter).ToListAsync();
            return messageList; 
        }
        /*
         * Método para deletar uma mensagem no banco de dados
         * params: {string pChatId}
         * return: {Task}
         */
        public async Task  DeleteMessageAsync(string pChatId)
        {
            var messageCollection = GetMessageCollection();
            var filter = Builders<MessageEntity>.Filter.Eq("chatId", pChatId);
            await messageCollection.DeleteOneAsync(filter);
        }
        /*
         * Método para atualizar uma mensagem no banco de dados
         * params: {string pChatId, Message pMessage}
         * return: {Task<MessageEntity>}
         */
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
