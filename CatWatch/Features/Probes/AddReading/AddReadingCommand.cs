namespace CatWatch.Features.Probes.AddReading;

public record AddReadingCommand(Guid ProbeId, decimal Temperature);
