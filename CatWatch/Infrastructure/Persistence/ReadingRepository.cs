using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Infrastructure.Exceptions;
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
        try
        {
            await _collection.InsertOneAsync(reading, cancellationToken: cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to add reading", ex);
        }
    }

    public async Task<IEnumerable<Reading>> GetByProbeIdAsync(Guid probeId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _collection.Find(r => r.ProbeId == probeId).ToListAsync(cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to get readings for probe {probeId}", ex);
        }
    }

    public async Task<IEnumerable<Reading>> GetLatestReadings(CancellationToken cancellationToken = default)
    {
        try
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
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to get latest readings", ex);
        }
    }
}