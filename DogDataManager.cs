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
        var imageUrl = await dogFetcher.FetchDogFromApi();
        if (imageUrl is null) return;

        var breedName = ExtractBreedName(imageUrl);
        if (string.IsNullOrEmpty(breedName)) return; // add logging
        
        var breed = await _dbContext.Breeds.FirstOrDefaultAsync(b => b.Name == breedName)
                    ?? _dbContext.Breeds.Add(new Breed { Name = breedName }).Entity;
    }


    public string ExtractBreedName(string imageUrl)
    {
        var breedParts = imageUrl.Split('/')[2];
        var segments = breedParts.Split('-');
        return segments.Length > 1 ? $"{segments[0]}-{segments[1]}" : breedParts[0].ToString();
    }
}