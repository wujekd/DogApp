using DogApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogApp;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<FavouriteBreed> FavouriteBreeds { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
}