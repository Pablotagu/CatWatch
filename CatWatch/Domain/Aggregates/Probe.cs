using CatWatch.Domain.Common;
using CatWatch.Domain.Entities;

namespace CatWatch.Domain.Aggregates;

public class Probe : Entity, IAggregateRoot
{
    public Guid ShelterId { get; private set; }
    public List<Reading> Readings { get; private set; }

    public Probe(Guid shelterId)
    {
        ShelterId = shelterId;
        Readings = new List<Reading>();
    }

    public void AddReading(Reading reading)
    {
        Readings.Add(reading);
    }
}