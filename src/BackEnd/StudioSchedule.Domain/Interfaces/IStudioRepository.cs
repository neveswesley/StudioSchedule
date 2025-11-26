using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Domain.Interfaces;

public interface IStudioRepository : IBaseRepository<Studio>
{
    Task<Studio> GetStudioWithRoom(Guid id);
    Task<List<Studio>> GetAllStudiosWithRoom();

}