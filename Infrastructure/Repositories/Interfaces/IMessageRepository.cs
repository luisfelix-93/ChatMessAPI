using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.Models;

namespace ChatMessAPI.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task InsertMessageAsync(Message pMessage);
        Task<List<MessageEntity>> GetMessageList(string pRoom);
        Task DeleteMessageAsync(string pChatId);
        Task<MessageEntity> UpdateMessageUpdate(string pChatId, Message pMessage);
    }
}
