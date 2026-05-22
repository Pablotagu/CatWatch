using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Readings.GetProbeReadings;


[ApiController]
public class GetProbeReadingsEndpoint : ControllerBase
{
    private readonly IMediator _mediator;


    public GetProbeReadingsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("api/probes/{probeId}/readings")]
    public async Task<IActionResult> Handle(Guid probeId, CancellationToken cancellationToken)
    {
        var readings = await _mediator.Send(new GetProbeReadingsQuery(probeId), cancellationToken);
        return Ok(readings);
    }
}