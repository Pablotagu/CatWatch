using CatWatch.Domain.Common;
using CatWatch.Domain.Entities;

namespace CatWatch.Domain.Aggregates;

public class Shelter : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    public Shelter(string name)
    {
        Name = name;
    }
}