using CatWatch.Domain.Aggregates;
using MediatR;


namespace CatWatch.Features.Shelters.GetShelters;


public record GetSheltersQuery : IRequest<IEnumerable<Shelter>>;