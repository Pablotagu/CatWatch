using CatWatch.Domain.Common;
using CatWatch.Domain.ValueObjects;

namespace CatWatch.Domain.Aggregates;

public record Reading :  IAggregateRoot
{
    public Guid ProbeId { get; init; }
    public DateTimeOffset Timestamp { get; init; }
    public required Temperature Temperature { get; init; }
}