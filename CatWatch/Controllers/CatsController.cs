using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<object>());
    }
}
