using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;

namespace ChatMessAPI.Services
{
    public class RoomService
    {
        #region attributes
        private readonly IRoomRepository _roomRepository;
        #endregion
        #region constructor
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        #endregion
        #region methods
        public async Task<ReturnDTO> CreateRoomService(Room pRoom)
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Message = "",
                Success = false,
                ResultObject = null
            };

            try
            {
                await _roomRepository.CreateRoomRepository(pRoom);
                returnDTO.Message = "Sala de conversação criada com sucesso";
                returnDTO.Success = true;
                returnDTO.ResultObject = pRoom;
            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro ao criar a sala de conversação, {ex.Message}";
            }
            return returnDTO;
        }

        public async Task<ReturnDTO> GetRoomListService()
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Message = "",
                Success = false,
                ResultObject = null
            };

            List<RoomEntity> roomList = new List<RoomEntity>();

            try
            {
                roomList = await _roomRepository.GetRoomListRepository();
                if (roomList.Count == 0)
                {
                    returnDTO.Message = "Não temos salas criadas";
                    returnDTO.Success = true;
                    return returnDTO;
                }

                returnDTO.ResultObject = roomList;
                returnDTO.Success = true;
                returnDTO.Message = "Lista de salas encontrada";

            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro ao buscar as salas de conversação, {ex.Message}";
            }

            return returnDTO;
        }

        #endregion
    }
}
