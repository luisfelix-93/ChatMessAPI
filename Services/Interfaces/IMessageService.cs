using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;

namespace ChatMessAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ReturnDTO> InsertMessage(Message pMessage);
        Task<ReturnDTO> GetMessagesByRoom(string pRoom);
        Task<ReturnDTO> DeleteMessage(string pChatId);
        Task<ReturnDTO> UpdateMessage(string pChatId, Message pMessage);
    }
}
   