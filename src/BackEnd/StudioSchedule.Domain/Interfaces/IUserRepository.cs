using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetAllUsersWithStudios();
    Task<User?> GetUserWithStudios(Guid id);
}