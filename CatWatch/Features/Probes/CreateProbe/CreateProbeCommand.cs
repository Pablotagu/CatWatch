using MediatR;

namespace CatWatch.Features.Probes.CreateProbe;

public record CreateProbeCommand(string Name, Guid ShelterId) : IRequest;