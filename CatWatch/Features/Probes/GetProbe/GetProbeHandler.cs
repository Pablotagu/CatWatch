using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;

namespace CatWatch.Features.Probes.GetProbe;

public class GetProbeHandler
{
    private readonly IProbeRepository _probeRepository;

    public GetProbeHandler(IProbeRepository probeRepository)
    {
        _probeRepository = probeRepository;
    }

    public async Task<Probe> HandleAsync(GetProbeQuery query, CancellationToken cancellationToken = default)
    {
        var probe = await _probeRepository.GetByIdAsync(query.Id, cancellationToken);
        if (probe == null)
        {
            throw new NotFoundException($"Probe with ID {query.Id} not found");
        }
        return probe;
    }
}