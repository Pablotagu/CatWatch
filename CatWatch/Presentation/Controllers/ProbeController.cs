using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProbeController : ControllerBase
{
    [HttpPost]
    [Route("api/[controller]/create")]
    
    public IActionResult CreateProbe()
    {
        return Ok(true);
    }

        [HttpPost]
    [Route("api/[controller]/{id}/readings}")]
    
    public IActionResult GetProbeReadings(int id)
    {
        return Ok(true);
    }

         [HttpPost]
    [Route("api/[controller]/{id}/status}")]
    
    public IActionResult GetProbeStatus(int id)
    {
        return Ok(true);
    }
}

