using Microsoft.EntityFrameworkCore;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Database;

namespace StudioSchedule.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    
    private readonly AppDbContext _context;


    public UserRepository(AppDbContext db, AppDbContext context) : base(db)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersWithStudios()
    {
        return await _context.Users.Include(s=>s.Studios).ToListAsync();
    }

    public async Task<User?> GetUserWithStudios(Guid id)
    {
        return await _context.Users.Include(s => s.Studios).FirstOrDefaultAsync(s => s.Id == id);
    }
}