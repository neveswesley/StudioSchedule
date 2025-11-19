using Microsoft.EntityFrameworkCore;
using StudioSchedule.Domain.Entities;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Database;

namespace StudioSchedule.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    
    private readonly AppDbContext _db;

    public BaseRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _db.AddAsync(entity);
        return entity;
    }

    public void UpdateAsync(T entity)
    {
        _db.Update(entity);
    }

    public void DeleteAsync(T entity)
    {
        _db.Remove(entity);
    }

    public async Task<T> GetByIdAsync(Guid? id)
    {
        var entity = await _db.Set<T>().FirstOrDefaultAsync(x=>x.Id == id);
        return entity;
    }

    public async Task<List<T>> GetAllAsync()
    {
        var entity = await _db.Set<T>().ToListAsync();
        return entity;
    }
}