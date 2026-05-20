using MediatR;

namespace CatWatch.Features.Probes.AddReading;

public record AddReadingCommand(Guid ProbeId, double Temperature) : IRequest;
