using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CatWatch.Features.Shelters.GetShelter;

[ApiController]
public class GetShelterEndpoint : ControllerBase
{
    private readonly IMediator _mediator;


    public GetShelterEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("api/shelters/{id}")]
    public async Task<IActionResult> Handle([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var shelter = await _mediator.Send(new GetShelterQuery(id), cancellationToken);
        return Ok(shelter);     
    }
}


public record GetShelterRequest(Guid ShelterId);