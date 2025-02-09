using Microsoft.AspNetCore.Mvc;

namespace DogApp.Controllers;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class DogController : ControllerBase
{
    private readonly AppDbContext _DbContext;

    public DogController(AppDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetDogs()
    {
        return Ok(await _DbContext.Dogs.ToListAsync());
    }
    
}