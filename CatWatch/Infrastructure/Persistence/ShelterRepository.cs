using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;
using CatWatch.Infrastructure.Exceptions;
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


    public async Task AddAsync(Shelter shelter, CancellationToken cancellationToken = default)
    {
        try
        {
            await _collection.InsertOneAsync(shelter, cancellationToken: cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to add shelter {shelter.Id}", ex);
        }
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


    public async Task<IEnumerable<Shelter>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _collection.Find(_ => true).ToListAsync(cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to get shelters", ex);
        }
    }

    public async Task<Shelter?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _collection.Find(r => r.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefaultAsync(cancellationToken);
        }
        catch (MongoException ex)
        {
            throw new RepositoryException($"Failed to get shelter {name}", ex);
        }
    }
}