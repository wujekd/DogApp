using DogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DogApp;

public class AppDbContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
}