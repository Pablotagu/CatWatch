using CatWatch.Domain.Aggregates;
using MediatR;

namespace CatWatch.Features.Readings.GetProbeReadings;


public record GetProbeReadingsQuery(Guid ProbeId) : IRequest<IEnumerable<Reading>>;