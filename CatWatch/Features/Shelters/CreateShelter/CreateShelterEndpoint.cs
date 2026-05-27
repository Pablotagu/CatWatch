using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Shelters.CreateShelter;

[ApiController]
public class CreateShelterEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateShelterEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/shelters")]
    public async Task<IActionResult> Handle([FromBody] CreateShelterRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateShelterCommand(request.Name), cancellationToken);
        return Created();     
    }
}

public record CreateShelterRequest(string Name);