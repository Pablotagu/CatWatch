using CatWatch.Domain.Aggregates;
using MediatR;


namespace CatWatch.Features.Readings.GetLatestReadings;


public record GetLatestReadingsQuery() : IRequest<IEnumerable<Reading>>;