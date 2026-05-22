using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;
using MediatR;

namespace CatWatch.Features.Readings.GetLatestReadings;

public class GetLatestReadingsHandler : IRequestHandler<GetLatestReadingsQuery, IEnumerable<Reading>>
{
    private readonly IReadingRepository _repository;


    public GetLatestReadingsHandler(IReadingRepository repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<Reading>> Handle(GetLatestReadingsQuery request, CancellationToken cancellationToken = default)
    {
        return await _repository.GetLatestReadingsAsync(cancellationToken);
    }
}