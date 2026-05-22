using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Readings.GetLatestReadings;


[ApiController]
public class GetLatestReadingsEndpoint : ControllerBase
{
    private readonly IMediator _mediator;


    public GetLatestReadingsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("api/readings/latest")]
    public async Task<IActionResult> Handle(CancellationToken cancellationToken)
    {
        var readings = await _mediator.Send(new GetLatestReadingsQuery(), cancellationToken);
        return Ok(readings);
    }
}