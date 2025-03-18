using ChatMessAPI.Infrastructure.Entities;
using ChatMessAPI.Infrastructure.Entities.DTO;
using ChatMessAPI.Infrastructure.Entities.Models;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;
using ChatMessAPI.Services.Interfaces;
using System.Reflection.Metadata;

namespace ChatMessAPI.Services
{
    public class RoomService : IRoomService
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
                    returnDTO.Message = "Não foi encontrada salas criadas";
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
        public async Task<ReturnDTO> GetRoomByNameService(string pRoom)
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Message = "",
                Success = false,
                ResultObject = null
            };

            RoomEntity room = new RoomEntity();

            try
            {
                room = await _roomRepository.GetRoomByNameRepository(pRoom);
                if (room == null)
                {
                    returnDTO.Message = $"Não foi encontrada a sala: {pRoom}";
                    returnDTO.Success = true;
                    return returnDTO;
                }

                returnDTO.Success = true;
                returnDTO.Message = "Sala encontrada!";
                returnDTO.ResultObject = room;

            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro ao buscar a sala de conversação, {ex.Message}";
            }

            return returnDTO;
        }

        public async Task<ReturnDTO> DeleteRoomByNameService(string pRoom)
        {
            ReturnDTO returnDTO = new ReturnDTO
            {
                Message = "",
                Success = false,
                ResultObject = null
            };

            try
            {
                await _roomRepository.DeleteRoomByNameRepository(pRoom);
                returnDTO.Success = true;
                returnDTO.Message = "Sala excluida com sucesso!";
            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro excluir a sala de conversação, {ex.Message}";
            }

            return returnDTO;
        }
        #endregion
    }
}
