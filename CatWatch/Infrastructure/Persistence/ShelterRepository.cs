using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;

namespace CatWatch.Infrastructure.Persistence;

public class ShelterRepository : IShelterRepository
{
    public Task<Shelter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}