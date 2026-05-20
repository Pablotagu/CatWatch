using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using MediatR;

namespace CatWatch.Features.Shelters.CreateShelter;

public class CreateShelterHandler : IRequestHandler<CreateShelterCommand>
{
    private readonly IShelterRepository _shelterRepository;

    public CreateShelterHandler(IShelterRepository shelterRepository)
    {
        _shelterRepository = shelterRepository;
    }

    public async Task Handle(CreateShelterCommand request, CancellationToken cancellationToken = default)
    {
        var shelter = new Shelter(request.Name);

        await _shelterRepository.AddAsync(shelter, cancellationToken);
    }
}