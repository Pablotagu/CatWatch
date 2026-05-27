using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Shelters.GetShelters;


[ApiController]
public class GetSheltersEndpoint : ControllerBase
{
    private readonly IMediator _mediator;


    public GetSheltersEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("api/shelters")]
    public async Task<IActionResult> Handle(CancellationToken cancellationToken)
    {
        var shelters = await _mediator.Send(new GetSheltersQuery(), cancellationToken);
        return Ok(shelters);     
    }
}