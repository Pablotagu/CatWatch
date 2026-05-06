using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Features.Probes.AddReading;

[ApiController]
public class AddReadingEndpoint : ControllerBase
{
    private readonly AddReadingHandler _handler;

    public AddReadingEndpoint(AddReadingHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("api/probes/{probeId:guid}/readings")]
    public async Task<IActionResult> Handle(Guid probeId, [FromBody] AddReadingRequest request, CancellationToken cancellationToken)
    {
        await _handler.HandleAsync(new AddReadingCommand(probeId, request.Temperature), cancellationToken);
        return Created();     
    }
}

public record AddReadingRequest(double Temperature);