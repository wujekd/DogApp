using DogApp.Models;
using Microsoft.AspNetCore.Identity;

namespace DogApp;


public class User : IdentityUser
{
    public string? Name { get; set; } 
    public List<FavouriteBreed> UserInterests { get; set; }
}