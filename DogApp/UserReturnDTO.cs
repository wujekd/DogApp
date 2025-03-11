using DogApp.Models;

namespace DogApp;

public class UserReturnDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Breed> FavouriteBreeds { get; set; }
}