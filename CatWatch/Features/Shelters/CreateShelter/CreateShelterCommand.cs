using MediatR;

namespace CatWatch.Features.Shelters.CreateShelter;

public record CreateShelterCommand(string Name) : IRequest;
