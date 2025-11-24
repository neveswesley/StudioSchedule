using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Database;

namespace StudioSchedule.Infrastructure.Repositories;

public class StudioRepository : BaseRepository<Studio>, IStudioRepository
{
    public StudioRepository(AppDbContext db) : base(db)
    {
    }

    public Task<Studio> GetAllUsersWithStudios()
    {
        throw new NotImplementedException();
    }
}