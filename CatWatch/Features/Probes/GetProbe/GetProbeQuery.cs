using CatWatch.Domain.Aggregates;
using MediatR;

namespace CatWatch.Features.Probes.GetProbe;

public record GetProbeQuery(Guid Id) : IRequest<Probe>;
