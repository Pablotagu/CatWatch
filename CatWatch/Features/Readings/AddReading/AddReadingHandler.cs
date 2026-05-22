using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Domain.ValueObjects;
using MediatR;

namespace CatWatch.Features.Readings.AddReading;

public class AddReadingHandler : IRequestHandler<AddReadingCommand>
{
    private readonly IReadingRepository  _readingRepository;
    private readonly IProbeRepository _probeRepository;

    public AddReadingHandler(IReadingRepository  readingRepository, IProbeRepository probeRepository)
    {
        _readingRepository = readingRepository;
        _probeRepository = probeRepository;
    }

    public async Task Handle(AddReadingCommand request, CancellationToken cancellationToken = default)
    {
        var probe = await _probeRepository.GetByIdAsync(request.ProbeId, cancellationToken);

        if (probe is null)
            throw new NotFoundException($"Probe with id {request.ProbeId} not found");

        var reading = new Reading
        {
            ProbeId = request.ProbeId,
            Timestamp = DateTimeOffset.Now,
            Temperature = new Temperature(request.Temperature)
        };

        await _readingRepository.AddAsync(reading, cancellationToken);  
    }
}