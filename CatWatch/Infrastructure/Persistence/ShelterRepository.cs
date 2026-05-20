using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using MongoDB.Driver;

namespace CatWatch.Infrastructure.Persistence;

public class ShelterRepository : IShelterRepository
{

    private readonly IMongoCollection<Shelter> _collection;


    public ShelterRepository(IMongoClient client, IConfiguration config)
    {
        var db = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _collection = db.GetCollection<Shelter>("shelters");
    }

    public async Task<Shelter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to get shelter {id}", ex);
        }
    }
}