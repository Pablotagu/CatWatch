using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Probes.GetProbe;

[ApiController]
public class GetProbeEndpoint : ControllerBase
{
    private readonly GetProbeHandler _handler;

    public GetProbeEndpoint(GetProbeHandler handler)
    {
        _handler = handler;
    }
    
    [HttpGet("api/probes/{probeId:guid}")]
    public async Task<IActionResult> Handle(Guid probeId, CancellationToken cancellationToken)
    {
        var probe = await _handler.HandleAsync(new GetProbeQuery(probeId), cancellationToken);
        return Ok(probe);
    }
}