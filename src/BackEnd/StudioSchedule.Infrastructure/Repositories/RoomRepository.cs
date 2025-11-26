using Microsoft.EntityFrameworkCore;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Database;

namespace StudioSchedule.Infrastructure.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext db, AppDbContext context) : base(db)
    {
        _context = context;
    }

    public async Task<List<Room>> GetAllRoomsWithStudios()
    {
        return await _context.Rooms.Include(x => x.Studio).ToListAsync();
    }

    public async Task<Room> GetRoomByIdWithStudios(Guid id)
    {
        return await _context.Rooms.Include(x => x.Studio).FirstOrDefaultAsync(x => x.Id == id);
    }
}