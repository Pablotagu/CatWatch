using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;
using CatWatch.Features.Shelters.GetShelters;
using NSubstitute;

namespace CatWatch.Tests.Features.Shelters.GetShelter;

public class GetSheltersHandlerTests
{
    private readonly IShelterRepository _shelterRepository = Substitute.For<IShelterRepository>();  

    [Fact]
    public async Task HandleAsync_ReturnsShelters()
    {
        var shelters = new List<Shelter>
        {
            new Shelter(Guid.NewGuid(), "Shelter 1"),
            new Shelter(Guid.NewGuid(), "Shelter 2")
        };

        _shelterRepository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(shelters);
        var handler = new GetSheltersHandler(_shelterRepository);
        var query = new GetSheltersQuery();
        var result = await handler.Handle(query, CancellationToken.None);
        Assert.Equal(shelters, result);
    }
}