using Microsoft.EntityFrameworkCore;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Database;

namespace StudioSchedule.Infrastructure.Repositories;

public class StudioRepository : BaseRepository<Studio>, IStudioRepository
{
    private readonly AppDbContext _context;

    public StudioRepository(AppDbContext db, AppDbContext context) : base(db)
    {
        _context = context;
    }

    public async Task<Studio> GetStudioWithRoom(Guid id)
    {
        return await _context.Studios.Include(s=>s.Rooms).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Studio>> GetAllStudiosWithRoom()
    {
        return await _context.Studios.Include(s => s.Rooms).ToListAsync();
    }

    public IQueryable<Studio> Query()
    {
        return _context.Studios.AsNoTracking();
    }
}