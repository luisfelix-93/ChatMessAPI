using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;

namespace ChatMessAPI.Services.Interfaces
{
    public interface IRoomService
    {
        Task<ReturnDTO> CreateRoomService(Room pRoom);
        Task<ReturnDTO> GetRoomListService();
        Task<ReturnDTO> GetRoomByNameService(string pRoom);
        Task<ReturnDTO> DeleteRoomByNameService(string pRoom);

    }
}
