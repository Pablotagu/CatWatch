using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Probes.GetProbe;

[ApiController]
public class GetProbeEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProbeEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("api/probes/{probeId:guid}")]
    public async Task<IActionResult> Handle(Guid probeId, CancellationToken cancellationToken)
    {
        var probe = await _mediator.Send(new GetProbeQuery(probeId), cancellationToken);
        return Ok(probe);
    }
}