using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Infrastructure.Exceptions;
using MongoDB.Driver;

namespace CatWatch.Infrastructure.Persistence;

public class ProbeRepository : IProbeRepository
{

    private readonly IMongoCollection<Probe> _collection;


    public ProbeRepository(IMongoClient client, IConfiguration config)
    {
        var db = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _collection = db.GetCollection<Probe>("probes");
    }


    public async Task<Probe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to get probe {id}", ex);
        }
    }


    public async Task AddAsync(Probe probe, CancellationToken cancellationToken = default)
    {
        try
        {
            await _collection.InsertOneAsync(probe, cancellationToken: cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to add probe {probe.Id}", ex);
        }
    }

}