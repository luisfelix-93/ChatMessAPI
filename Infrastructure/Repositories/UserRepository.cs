using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatMessAPI.Infrastructure.Repositories
{
    public class UserRepository
    {
        #region attributes
        private readonly DatabaseHelper _databaseHelper;
        #endregion
        #region constructor
        public UserRepository(IOptions<DatabaseHelper> databaseHelper)
        {
            _databaseHelper = databaseHelper.Value;
        }
        #endregion
        #region methods
        private IMongoCollection<UserEntity> GetUserCollection()
        {
            var mongoClient = new MongoClient(_databaseHelper.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_databaseHelper.DatabaseName);
            var userCollection = mongoDatabase.GetCollection<UserEntity>(_databaseHelper.UserCollection);
            return userCollection;
        }

        public async Task InsertUserAsync(User pUser)
        {
            UserEntity userEntity = new UserEntity
            {
                UserId = ObjectId.GenerateNewId().ToString(),
                Username = pUser.Username,
                Room = pUser.Room,
                SocketId = pUser.SocketId,
                JoinedAt = DateTime.Now
            };
            var userCollection = GetUserCollection();
            await userCollection.InsertOneAsync(userEntity);
        }

        public async Task<UserEntity> GetUserAsync(string pSocketId)
        {
            var userCollection = GetUserCollection();
            var filter = Builders<UserEntity>.Filter.Eq("socketId", pSocketId);
            var user = await userCollection.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        public async Task DeleteUserAsync(string pRoom)
        {
            var userCollection = GetUserCollection();
            var filter = Builders<UserEntity>.Filter.Eq("room", pRoom);
            await userCollection.DeleteManyAsync(filter);
        }

        public async Task<UserEntity[]> GetUserListAsync(string pRoom)
        {
            var userCollection = GetUserCollection();
            var filter = Builders<UserEntity>.Filter.Eq("room", pRoom);
            var userList = await userCollection.Find(filter).ToListAsync();
            return userList.ToArray();
        }

        #endregion
    }
}
