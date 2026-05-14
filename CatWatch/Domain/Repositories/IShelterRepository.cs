using CatWatch.Domain.Aggregates;

namespace CatWatch.Domain.Repositories;

public interface IShelterRepository
{
    Task<Shelter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);  

}