using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;

namespace ChatMessAPI.Services
{
    public class MessageService
    {
        #region attributes
        private readonly IMessageRepository _messageRepository;
        #endregion
        #region constructor
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        #endregion
        #region methods
        /*
         * Método para chamar o método de inserção de mensagem do repositório
         * params : {Message pMessage}
         * return : {Task<ReturnDTO>}
         */
        public async Task<ReturnDTO> InsertMessage(Message pMessage)
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Success = false,
                Message = "",
                ResultObject = null
            };

            try
            {
                await _messageRepository.InsertMessageAsync(pMessage);
                returnDTO.Success = true;
                returnDTO.Message = "Messagem inserida com sucesso!";
                returnDTO.ResultObject = pMessage;
            }
            catch (Exception ex)
            {
                returnDTO.Message = ex.Message;
            }
            return returnDTO;
        }
        /*
         * Método para chamar o método de recuperação de mensagens por sala do repositório
         * params: {string pRoom}
         * return: {Task<ReturnDTO>}
         */
        public async Task<ReturnDTO> GetMessagesByRoom(string pRoom)
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Success = false,
                Message = "",
                ResultObject = null
            };
            List<MessageEntity> messageList = new List<MessageEntity>();
            try
            {
                var messagesArray = await _messageRepository.GetMessageList(pRoom);
                messageList = messagesArray.ToList();
                returnDTO.Success = true;
                returnDTO.Message = "Mensagens recuperadas com sucesso!";
                returnDTO.ResultObject = messageList;
            }
            catch (Exception ex)
            {
                returnDTO.Message = ex.Message;
            }
            return returnDTO;
        }
        /*
         * Método para chamar o método de deleção de mensagem do repositório
         * params: {string pChatId}
         * return: {Task<ReturnDTO>}
         */
        public async Task<ReturnDTO> DeleteMessage(string pChatId)
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Success = false,
                Message = "",
                ResultObject = null
            };
            try
            {
                await _messageRepository.DeleteMessageAsync(pChatId);
                returnDTO.Success = true;
                returnDTO.Message = "Mensagem deletada com sucesso!";
            }
            catch (Exception ex)
            {
                returnDTO.Message = ex.Message;
            }
            return returnDTO;
        }
        /*
         * Método para chamar o método de atualização de mensagem do repositório
         * params: {string pChatId, Message pMessage}
         * return: {Task<ReturnDTO>}
         */
        public async Task<ReturnDTO> UpdateMessage(string pChatId, Message pMessage)
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Success = false,
                Message = "",
                ResultObject = null
            };
            try
            {
                var messageEntity = await _messageRepository.UpdateMessageUpdate(pChatId, pMessage);
                returnDTO.Success = true;
                returnDTO.Message = "Mensagem atualizada com sucesso!";
                returnDTO.ResultObject = messageEntity;
            }
            catch (Exception ex)
            {
                returnDTO.Message = ex.Message;
            }
            return returnDTO;
        }

        #endregion
    }
}
