using CatWatch.Domain.Aggregates;
using MediatR;

namespace CatWatch.Features.Shelters.GetShelter;

public record GetShelterQuery(Guid Id) : IRequest<Shelter>;
