using System.Text.Json.Serialization;
using DogApp.Models;
using DogApp.NetworkServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Controllers;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class DogController : ControllerBase
{
    private readonly AppDbContext _DbContext;
    private readonly IFetchDog _FetchDog;
    private readonly UserManager<User> _UserManager;

    public DogController(AppDbContext dbContext, IFetchDog fetchDog)
    {
        _DbContext = dbContext;
        _FetchDog = fetchDog;
    }

    
    // GET ALL DOGS
    [HttpGet]
    public async Task<IActionResult> GetDogs()
    {
        return Ok(await _DbContext.Dogs.ToListAsync());
    }
    

    // GET A DOG FROM EXTERNAL API
    [HttpGet("random-from-web-api")]
    public async Task<IActionResult> RandomApi()
    {
        var DogImageUrl = await _FetchDog.FetchDogFromApi();
        
        // HANDLE NOT FOUND
        
        return Ok(DogImageUrl);
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
    
    

    
    
    
    // PASS MOST FREQUENTLY FAVOURITED BREEDS TO SUGGEST BREEDS ON ACCOUNT CREATION
}