using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using MediatR;

namespace CatWatch.Features.Probes.GetProbe;

public class GetProbeHandler : IRequestHandler<GetProbeQuery, Probe>
{
    private readonly IProbeRepository _probeRepository;

    public GetProbeHandler(IProbeRepository probeRepository)
    {
        _probeRepository = probeRepository;
    }

    public async Task<Probe> Handle(GetProbeQuery request, CancellationToken cancellationToken = default)
    {
        var probe = await _probeRepository.GetByIdAsync(request.Id, cancellationToken);
        if (probe == null)
        {
            throw new NotFoundException($"Probe with ID {request.Id} not found");
        }
        return probe;
    }

}