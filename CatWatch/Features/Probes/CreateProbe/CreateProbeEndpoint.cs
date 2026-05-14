using System.ComponentModel.Design;
using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Domain.ValueObjects;

namespace CatWatch.Features.Probes.CreateProbe;

public class CreateProbeHandler
{
    private readonly IProbeRepository _probeRepository;
    private readonly IShelterRepository _shelterRepository;


    public CreateProbeHandler(IProbeRepository probeRepository, IShelterRepository shelterRepository)
    {
        _probeRepository = probeRepository;
        _shelterRepository = shelterRepository;
    }


    public async Task HandleAsync(CreateProbeCommand command, CancellationToken cancellationToken = default)
    {
        var shelter = await _shelterRepository.GetByIdAsync(command.ShelterId, cancellationToken);

        if (shelter is null)
            throw new NotFoundException($"Shelter with id {command.ShelterId} not found");

        var probe = new Probe(command.ShelterId, command.Name);

        await _probeRepository.AddAsync(probe, cancellationToken);
    }
}