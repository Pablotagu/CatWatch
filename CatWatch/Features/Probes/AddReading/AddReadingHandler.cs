using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Domain.ValueObjects;

namespace CatWatch.Features.Probes.AddReading;

public class AddReadingHandler
{
    private readonly IReadingRepository  _readingRepository;
    private readonly IProbeRepository _probeRepository;

    public AddReadingHandler(IReadingRepository  readingRepository, IProbeRepository probeRepository)
    {
        _readingRepository = readingRepository;
        _probeRepository = probeRepository;
    }

    public async Task HandleAsync(AddReadingCommand command, CancellationToken cancellationToken = default)
    {
        var probe = await _probeRepository.GetByIdAsync(command.ProbeId, cancellationToken);

        if (probe is null)
            throw new NotFoundException($"Probe with id {command.ProbeId} not found");

        var reading = new Reading
        {
            ProbeId = command.ProbeId,
            Timestamp = DateTimeOffset.Now,
            Temperature = new Temperature(command.Temperature)
        };

        await _readingRepository.AddAsync(reading, cancellationToken);
    }
}