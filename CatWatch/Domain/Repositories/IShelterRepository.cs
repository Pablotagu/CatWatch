using CatWatch.Domain.Aggregates;

namespace CatWatch.Domain.Repositories;

public interface IShelterRepository
{
    Task AddAsync(Shelter shelter, CancellationToken cancellationToken = default);
    Task<IEnumerable<Shelter>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Shelter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);  
    Task<Shelter?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}