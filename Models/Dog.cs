using System.Text.Json.Serialization;

namespace DogApp.Models;

public class Dog
{
    [JsonIgnore]
    public int Id { get; set; } //not required because no DTO yet
    
    
    public required string ImageSrc { get; set; }
    
    public required int BreedId { get; set; }
    [JsonIgnore]
    public Breed? Breed { get; set; } // add required with DTO
    // make nullable so it wont check it when parsing the request (its added by a controller later)
}