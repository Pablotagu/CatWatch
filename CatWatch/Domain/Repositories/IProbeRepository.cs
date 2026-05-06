using CatWatch.Domain.Aggregates;

namespace CatWatch.Domain.Repositories;

public interface IProbeRepository
{
    Task<Probe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);  
}