namespace DogApp.Models;

public class FavouriteBreed
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
    public int BreedId { get; set; }
    public Breed Breed { get; set; }
}