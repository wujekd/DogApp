using DogApp.Models;
using DogApp.NetworkServices;
using Microsoft.EntityFrameworkCore;

namespace DogApp;

public class DogDataManager
{
    private readonly IFetchDog dogFetcher;
    private readonly AppDbContext _dbContext;


    public DogDataManager(IFetchDog fetchDog, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        dogFetcher = fetchDog;
    }

    public async Task AddDogFromAPI()
    {
        string imageUrl = await dogFetcher.FetchDogFromApi();
        if (imageUrl is null) return;

        var breedName = ExtractBreedName(imageUrl);
        if (string.IsNullOrEmpty(breedName)) return; // add logging
        
        var breed = await _dbContext.Breeds.FirstOrDefaultAsync(b => b.Name == breedName)
                    ?? _dbContext.Breeds.Add(new Breed { Name = breedName }).Entity;
        var dog = new Dog
        {
            Breed = breed,
            ImageSrc = imageUrl
        };
        
        _dbContext.Dogs.Add(dog);
        await _dbContext.SaveChangesAsync();
    }


    public string ExtractBreedName(string imageUrl)
    {
        var breedParts = imageUrl.Split('/')[4];
        Console.WriteLine($"HERED THE BREED PARTS: {breedParts}");
        var segments = breedParts.Split('-');
        return segments.Length > 1 ? $"{segments[1]} {segments[0]}" : breedParts;
    }
}