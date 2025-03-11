using System.Text.Json.Serialization;
using DogApp.Models;
using DogApp.NetworkServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Controllers;
using Microsoft.EntityFrameworkCore;

[Route("dogs")]
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
    [HttpGet("all")]
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
    
    
    // ADD A NEW DOG
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
    
    
// GET ALL BREEDS WITH DOG COUNT
    [Authorize]
    [HttpGet("breeds")]
    public async Task<IActionResult> GetBreeds()
    {
        var breeds = await _DbContext.Breeds.ToListAsync();
        var response = new List<BreedReturnDTO>();

        foreach (var breed in breeds)
        {
            var dogCount = await _DbContext.Dogs.CountAsync(d => d.BreedId == breed.Id);

            response.Add(new BreedReturnDTO
            {
                Id = breed.Id,
                Name = breed.Name,
                DogCount = dogCount
            });
        }
        return Ok(response);
    }
    
    
    //GET ALL DOGS OF A GIVEN BREED
    [HttpGet("breeds/{breedId}")]
    public async Task<IActionResult> GetBreed(int breedId)
    {
        var breed = await _DbContext.Breeds.FindAsync(breedId);
        var dogs = await _DbContext.Dogs.Where(d => d.BreedId == breedId).ToListAsync();

        var response = new BreedReturnDTO
        {
            Id = breed.Id,
            Name = breed.Name,
            Dogs = dogs
        };
        return Ok(response);
    }
    

    
    
    
    // PASS MOST FREQUENTLY FAVOURITED BREEDS TO SUGGEST BREEDS ON ACCOUNT CREATION
}