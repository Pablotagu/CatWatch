using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using MediatR;

namespace CatWatch.Features.Probes.CreateProbe;

public class CreateProbeHandler : IRequestHandler<CreateProbeCommand>

{
    private readonly IProbeRepository _probeRepository;
    private readonly IShelterRepository _shelterRepository;


    public CreateProbeHandler(IProbeRepository probeRepository, IShelterRepository shelterRepository)
    {
        _probeRepository = probeRepository;
        _shelterRepository = shelterRepository;
    }

    public async Task Handle(CreateProbeCommand request, CancellationToken cancellationToken)
    {
         var shelter = await _shelterRepository.GetByIdAsync(request.ShelterId, cancellationToken);

        if (shelter is null)
            throw new NotFoundException($"Shelter with id {request.ShelterId} not found");

        var probe = new Probe(request.ShelterId, request.Name);

        await _probeRepository.AddAsync(probe, cancellationToken);
    }
}