using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;
using MongoDB.Driver;

namespace CatWatch.Infrastructure.Persistence;

public class ReadingRepository : IReadingRepository
{

    private readonly IMongoCollection<Reading> _collection;

    public ReadingRepository(IMongoClient client, IConfiguration config)
    {
        var db = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _collection = db.GetCollection<Reading>("readings");
    }

    public async Task AddAsync(Reading reading, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(reading, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Reading>> GetByProbeIdAsync(Guid probeId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(r => r.ProbeId == probeId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Reading>> GetLatestReadings(CancellationToken cancellationToken = default)
    {
        var pipeline = new EmptyPipelineDefinition<Reading>()
            .Sort(Builders<Reading>.Sort.Descending(r => r.Timestamp))
            .Group(
                r => r.ProbeId,
                g => new Reading
                {
                    ProbeId = g.Key,
                    Timestamp = g.First().Timestamp,
                    Temperature = g.First().Temperature
                });
        return await _collection.Aggregate(pipeline).ToListAsync(cancellationToken);
    }
}