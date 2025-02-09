using System.Text.Json.Serialization;
using DogApp.Models;
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


    
    [HttpPost("add")]
    public async Task<IActionResult> AddDog([FromBody] Dog dog)
    {
        var newDog = await _DbContext.Dogs.AddAsync(dog); 
        // newDog is a Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> object.
        var breed = await _DbContext.Breeds.FindAsync(dog.BreedId);
        newDog.Entity.Breed = breed;
        
        await _DbContext.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetDogs), new { id = newDog.Entity.Id }, newDog.Entity);
    }
    
    

    [HttpGet("breeds")]
    public async Task<IActionResult> GetBreeds()
    {
        return Ok(await _DbContext.Breeds.ToListAsync());
    }
    
}