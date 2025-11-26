using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Domain.Interfaces;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<List<Room>> GetAllRoomsWithStudios();
    Task<Room> GetRoomByIdWithStudios(Guid id);
}