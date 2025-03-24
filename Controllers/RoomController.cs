using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatMessAPI.Controllers
{
    [ApiController]
    [Route("Room")]
    public class RoomController : ControllerBase
    {
        #region attributes
        private readonly IRoomService _roomService;
        #endregion
        #region constructor
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        #endregion
        #region methods
        [HttpPost]
        public async Task<IActionResult> CreateRoom(Room pRoom)
        {
            ReturnDTO returnDTO = new ReturnDTO();
            returnDTO = await _roomService.CreateRoomService(pRoom);
            if (!returnDTO.Success)
            {
                return new BadRequestObjectResult(returnDTO);
            }

            return new OkObjectResult(returnDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomList()
        {
            ReturnDTO returnDTO = await _roomService.GetRoomListService();
            if(returnDTO.Success)
            {
                if(returnDTO.ResultObject != null)
                {
                    return new OkObjectResult(returnDTO);
                }
                return new NotFoundObjectResult(returnDTO);
            }

            return new BadRequestObjectResult(returnDTO);
        }

        [Route("{pNmRoom}")]
        [HttpGet]
        public async Task<IActionResult> GetRoomByName(string pNmRoom)
        {
            ReturnDTO returnDTO = await _roomService.GetRoomByNameService(pNmRoom);
            if (returnDTO.Success)
            {
                if (returnDTO.ResultObject != null)
                {
                    return new OkObjectResult(returnDTO);
                }
                return new NotFoundObjectResult(returnDTO);
            }
            return new BadRequestObjectResult(returnDTO);
        } 
        #endregion
    }
}
