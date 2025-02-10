using DogApp.Models;

namespace DogApp;

public class BreedReturnDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? DogCount { get; set; }
    
    public List<Dog>? Dogs { get; set; }
}