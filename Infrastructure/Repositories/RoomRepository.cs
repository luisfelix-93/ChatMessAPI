using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Helpers;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatMessAPI.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        #region attributes
        private readonly DatabaseHelper _databaseHelper;
        #endregion
        #region constructor
        public RoomRepository(IOptions<DatabaseHelper> databaseHelper)
        {
            _databaseHelper = databaseHelper.Value;
        }
        #endregion
        #region methods
        private IMongoCollection<RoomEntity> GetRoomCollection()
        {
            var mongoClient = new MongoClient(_databaseHelper.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_databaseHelper.DatabaseName);
            var room = mongoDatabase.GetCollection<RoomEntity>(_databaseHelper.RoomCollection);

            return room;

        }

        public async Task CreateRoomRepository(Room pRoom)
        {
            var roomCollection = this.GetRoomCollection();
            RoomEntity room = new RoomEntity
            {
                RoomId = ObjectId.GenerateNewId().ToString(),
                Room = pRoom.NmRoom,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd")
            };

            await roomCollection.InsertOneAsync(room);
        }

        public async Task<List<RoomEntity>> GetRoomListRepository()
        {
            var roomCollection = this.GetRoomCollection();
            var roomList = await roomCollection.FindAsync(_ => true);
            return await roomList.ToListAsync();
        }

        public async Task<RoomEntity> GetRoomByNameRepository(string pNmRoom)
        {
            var roomCollection = this.GetRoomCollection();
            var filter = Builders<RoomEntity>.Filter.Eq(r => r.Room, pNmRoom);
            var roomCursor = await roomCollection.FindAsync(filter);
            var roomEntity = await roomCursor.FirstOrDefaultAsync();
            return roomEntity;
        }

        public async Task DeleteRoomByNameRepository(string pNmRoom)
        {
            var roomCollection = this.GetRoomCollection();
            var filter = Builders<RoomEntity>.Filter.Eq(r => r.Room, pNmRoom);
            await roomCollection.FindOneAndDeleteAsync(filter);
        }
        #endregion
    }
}
