using CatWatch.Domain.Common;
using CatWatch.Domain.Entities;

namespace CatWatch.Domain.Aggregates;

public class Probe : Entity, IAggregateRoot
{
    public Guid ShelterId { get; private set; }
    public string Name { get; private set; }

    public Probe(Guid shelterId, string name)
    {
        ShelterId = shelterId;
        Name = name;
    }
}