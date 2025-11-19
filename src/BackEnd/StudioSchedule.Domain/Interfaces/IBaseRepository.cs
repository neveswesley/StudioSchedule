using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
    Task<T> GetByIdAsync(Guid? id);
    Task<List<T>> GetAllAsync();
}