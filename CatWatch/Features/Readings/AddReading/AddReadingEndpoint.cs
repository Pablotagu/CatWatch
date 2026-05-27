using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Readings.AddReading;

[ApiController]
public class AddReadingEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public AddReadingEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize]
    [HttpPost("api/probes/{probeId:guid}/readings")]
    public async Task<IActionResult> Handle(Guid probeId, [FromBody] AddReadingRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AddReadingCommand(probeId, request.Temperature), cancellationToken);
        return Created();     
    }
}

public record AddReadingRequest(double Temperature);