namespace DogApp.Models;

public class Dog
{
    public required int Id { get; set; }
    public required string ImageSrc { get; set; }
    public required int BreedId { get; set; }
}