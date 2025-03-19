using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.Models;

namespace ChatMessAPI.Infrastructure.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task CreateRoomRepository(Room pRoom);
        Task<List<RoomEntity>> GetRoomListRepository();
        Task<RoomEntity> GetRoomByNameRepository(string pNmRoom);
        Task DeleteRoomByNameRepository(string pNmRoom);
    }
}
