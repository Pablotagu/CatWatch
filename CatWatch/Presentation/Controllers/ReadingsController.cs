using Microsoft.AspNetCore.Mvc;

namespace CatWatch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create()
    {
        return Ok(true);
    }
}
