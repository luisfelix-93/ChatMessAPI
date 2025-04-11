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
        private ILogger<RoomService> _logger;
        #endregion
        #region constructor
        public RoomService(IRoomRepository roomRepository, ILogger<RoomService> logger)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }
        #endregion
        #region methods
        /*
         * Serviço assíncrono de criação de sala
         * @param pRoom {Room} - Objeto de sala
         * @returns Task<ReturnDTO> - objeto de retorno
         */
        public async Task<ReturnDTO> CreateRoomService(Room pRoom)
        {
            _logger.LogInformation("CreateRoomService => Start");
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
                _logger.LogInformation($"CreateRoomService => Sala criada com sucesso: ResultObject: {pRoom}");
            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro ao criar a sala de conversação, {ex.Message}";
                _logger.LogInformation($"CreateRoomService => Erro ao criar a sala de conversação: {ex.Message}");
            }
            return returnDTO;
        }
        /*
         * Serviço assíncrono de listagem de salas
         * @returns Task<ReturnDTO> - objeto de retorno
         */
        public async Task<ReturnDTO> GetRoomListService()
        {
            _logger.LogInformation("GetRoomListService => Start");
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

                    _logger.LogInformation("GetRoomListService => Não foi encontrada salas criadas");
                    return returnDTO; // Move this line after the log statement
                }

                returnDTO.ResultObject = roomList;
                returnDTO.Success = true;
                returnDTO.Message = "Lista de salas encontrada";
                _logger.LogInformation($"GetRoomListService => {roomList.Count} salas encontradas");
            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro ao buscar as salas de conversação, {ex.Message}";
                _logger.LogInformation($"GetRoomListService => Erro ao buscar as salas de conversação: {ex.Message}");
            }

            return returnDTO;
        }
        /*
         * Serviço assícrono de busca de sala por nome
         * @params pRoom {string} - nome da sala
         * returns Task<ReturnDTO> - objeto de retorno
         */
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
                _logger.LogInformation($"GetRoomByNameService => Sala encontrada: {room}");

            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro ao buscar a sala de conversação, {ex.Message}";
                _logger.LogInformation($"GetRoomByNameService => Erro ao buscar a sala de conversação: {ex.Message}");
            }

            return returnDTO;
        }
        /*
         * Serviço assíncrono de exclusão de sala por nome
         * @params pRoom {string} - nome da sala
         * @returns Task<ReturnDTO> - objeto de retorno
         */
        public async Task<ReturnDTO> DeleteRoomByNameService(string pRoom)
        {
            _logger.LogInformation("DeleteRoomByNameService => Start");
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
                _logger.LogInformation($"DeleteRoomByNameService => Sala excluida com sucesso: {pRoom}");
            }
            catch (Exception ex)
            {
                returnDTO.Message = $"Erro excluir a sala de conversação, {ex.Message}";
                _logger.LogInformation($"DeleteRoomByNameService => Erro ao excluir a sala de conversação: {ex.Message}");
            }

            return returnDTO;
        }
        #endregion
    }
}
