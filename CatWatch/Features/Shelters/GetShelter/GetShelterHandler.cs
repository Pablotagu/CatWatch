using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using MediatR;

namespace CatWatch.Features.Shelters.GetShelter;

public class GetShelterHandler : IRequestHandler<GetShelterQuery, Shelter>
{
    private readonly IShelterRepository _shelterRepository;

    public GetShelterHandler(IShelterRepository shelterRepository)
    {
        _shelterRepository = shelterRepository;
    }

    public async Task<Shelter> Handle(GetShelterQuery request, CancellationToken cancellationToken = default)
    {
        var shelter = await _shelterRepository.GetByIdAsync(request.Id, cancellationToken);
        if (shelter == null)
        {
            throw new NotFoundException($"Shelter with ID {request.Id} not found");
        }
        return shelter;
    }

}