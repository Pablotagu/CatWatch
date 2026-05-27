using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Probes.CreateProbe;

[ApiController]
public class CreateProbeEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateProbeEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("api/probes")]
    public async Task<IActionResult> Handle([FromBody] CreateProbeRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateProbeCommand(request.Name, request.ShelterId), cancellationToken);
        return Created();     
    }
}


public record CreateProbeRequest(string Name, Guid ShelterId);
