using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;
using MediatR;

namespace CatWatch.Features.Shelters.GetShelters;

public class GetSheltersHandler : IRequestHandler<GetSheltersQuery, IEnumerable<Shelter>>
{
    private readonly IShelterRepository _shelterRepository;


    public GetSheltersHandler(IShelterRepository shelterRepository)
    {
        _shelterRepository = shelterRepository;
    }


    public async Task<IEnumerable<Shelter>> Handle(GetSheltersQuery request, CancellationToken cancellationToken = default)
    {
        var shelters = await _shelterRepository.GetAllAsync(cancellationToken);
        return shelters;
    }

}