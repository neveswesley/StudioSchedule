using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Database;

namespace StudioSchedule.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext db) : base(db)
    {
    }
}