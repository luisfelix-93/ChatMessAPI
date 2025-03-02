using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;
using ChatMessAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatMessAPI.Controllers
{
    [ApiController]
    [Route("Message")]
    public class MessageController : ControllerBase
    {
        #region attributes
        private readonly IMessageService _messageService;
        #endregion
        #region constructor
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        #endregion
        #region methods
        [HttpPost]
        public async Task<IActionResult> InsertMessageAsync([FromBody] Message pMessage )
        {
            ReturnDTO returnDTO = await _messageService.InsertMessage(pMessage);
            if (returnDTO.Success)
            {
                return new OkObjectResult(returnDTO);
            }
            return new BadRequestObjectResult(returnDTO);
        }
        [HttpGet]
        [Route("room/{pRoom}")]
        public async Task<IActionResult> FindMessageByRoom(string pRoom)
        {
            ReturnDTO returnDTO = await _messageService.GetMessagesByRoom(pRoom);
            if(returnDTO.Success)
            {
                if (returnDTO.ResultObject == null)
                {
                    return new NotFoundObjectResult(returnDTO);
                }
                return new OkObjectResult(returnDTO);
            }

            return new BadRequestObjectResult(returnDTO);
        }

        [HttpDelete]
        [Route("message/{pMessageId}")]
        public async Task<IActionResult> DeleteMessage(string pMessageId)
        {
            ReturnDTO returnDTO = await _messageService.DeleteMessage(pMessageId);
            if (returnDTO.Success)
            {
                return new OkObjectResult(returnDTO);
            }
            return new BadRequestObjectResult(returnDTO);
        }

        [HttpPut]
        [Route("message/{pMessageId}")]
        public async Task<IActionResult> UpdateMessage(string pMessageId, [FromBody] Message pMessage)
        {
            ReturnDTO returnDTO = await _messageService.UpdateMessage(pMessageId, pMessage);
            if (returnDTO.Success)
            {
                return new OkObjectResult(returnDTO);
            }
            return new BadRequestObjectResult(returnDTO);
        }
        #endregion
    }
}
