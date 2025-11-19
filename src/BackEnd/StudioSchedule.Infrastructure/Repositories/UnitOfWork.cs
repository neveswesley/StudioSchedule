using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Database;

namespace StudioSchedule.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync();
    }
}