using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Repositories;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;
using ChatMessAPI.Services.Interfaces;

namespace ChatMessAPI.Services
{
    public class MessageService : IMessageService
    {
        #region attributes
        private readonly IMessageRepository _messageRepository;
        private ILogger<MessageService> _logger;
        #endregion
        #region constructor
        public MessageService(IMessageRepository messageRepository, ILogger<MessageService> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
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

            _logger.LogInformation("InsertMessage => Start");

            try
            {
                await _messageRepository.InsertMessageAsync(pMessage);
                returnDTO.Success = true;
                returnDTO.Message = "Messagem inserida com sucesso!";
                returnDTO.ResultObject = pMessage;

                _logger.LogInformation($"InsertMessage => Mensagem inserida na base com sucesso: ResultObject: {pMessage}");
            }
            catch (Exception ex)
            {
                returnDTO.Message = ex.Message;
                _logger.LogInformation($"InsertMessage: Error => {ex.Message}");

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
            _logger.LogInformation($"GetMessagesByRoom => Start | Sala : {pRoom}");
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
                _logger.LogInformation($"GetMessagesByRoom => {messagesArray.Count} mensagens na sala {pRoom}");
            }
            catch (Exception ex)
            {
                returnDTO.Message = ex.Message;
                _logger.LogInformation($"GetMessagesByRoom => Erro ao retornar as mensagens da sala: {ex.Message}");
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
            _logger.LogInformation("DeleteMessage => Start!");
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
                _logger.LogInformation($"DeleteMessage => {returnDTO.Message}");
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
            _logger.LogInformation("UpdateMessage => Start!");
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

                _logger.LogInformation($"UpdateMessage => Mensagem atualizada com sucesso: {returnDTO.Message}");
            }
            catch (Exception ex)
            {
                returnDTO.Message = ex.Message;
                _logger.LogInformation($"UpdateMessage => Erro ao atualizar a mensagem: {ex.Message}");

            }
            return returnDTO;
        }

        #endregion
    }
}
