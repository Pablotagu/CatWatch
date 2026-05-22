using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using MediatR;

namespace CatWatch.Features.Readings.GetProbeReadings;


public class GetProbeReadingsHandler : IRequestHandler<GetProbeReadingsQuery, IEnumerable<Reading>>
{
    private readonly IReadingRepository _repository;
    private readonly IProbeRepository _probeRepository;


    public GetProbeReadingsHandler(IReadingRepository repository, IProbeRepository probeRepository)
    {
        _repository = repository;
        _probeRepository = probeRepository;
    }


    public async Task<IEnumerable<Reading>> Handle(GetProbeReadingsQuery request, CancellationToken cancellationToken = default)
    {
        var probe = await _probeRepository.GetByIdAsync(request.ProbeId, cancellationToken);
        if (probe == null)
        {
            throw new NotFoundException($"Probe with ID {request.ProbeId} not found.");
        }

        return await _repository.GetByProbeIdAsync(request.ProbeId, cancellationToken);
    }
}