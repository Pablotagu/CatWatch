using CatWatch.Domain.Aggregates;

namespace CatWatch.Domain.Repositories;

public interface IReadingRepository
{
    Task AddAsync(Reading reading, CancellationToken cancellationToken = default);
    Task<IEnumerable<Reading>> GetByProbeIdAsync(Guid probeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Reading>> GetLatestReadings(CancellationToken cancellationToken = default);

}