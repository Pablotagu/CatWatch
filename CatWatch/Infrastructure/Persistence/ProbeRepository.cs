using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;

namespace CatWatch.Infrastructure.Persistence;

public class ProbeRepository : IProbeRepository
{
    public Task<Probe> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}