using MediatR;

namespace CatWatch.Features.Readings.AddReading;

public record AddReadingCommand(Guid ProbeId, double Temperature) : IRequest;
