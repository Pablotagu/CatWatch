using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Probes.CreateProbe;

[ApiController]
public class CreateProbeEndpoint : ControllerBase
{
    private readonly CreateProbeHandler _handler;


    public CreateProbeEndpoint(CreateProbeHandler handler)
    {
        _handler = handler;
    }


    [Authorize(Policy = "ApiKeyPolicy")]
    [HttpPost("api/probes")]
    public async Task<IActionResult> Handle([FromBody] CreateProbeRequest request, CancellationToken cancellationToken)
    {
        await _handler.HandleAsync(new CreateProbeCommand(request.Name, request.ShelterId), cancellationToken);
        return Created();     
    }
}


public record CreateProbeRequest(string Name, Guid ShelterId);