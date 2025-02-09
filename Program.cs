using DogApp;
using DogApp.NetworkServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registers with DI to manage HttpClient (no multiple instances per request)
builder.Services.AddHttpClient<IFetchDog, FetchDog>();


// database <-> ORM (Entity Framework) DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        "server=localhost;port=3306;database=dog_app;user=project_user;password=admin123;",
        ServerVersion.AutoDetect("server=localhost;port=3306;database=dog_app;user=project_user;password=admin123;")
        )
    );


var app = builder.Build();

// run on startup
using (var scope = app.Services.CreateScope())
{
    var dogManager = scope.ServiceProvider.GetRequiredService<DogManagerService>();
    await dogManager.AddFetchedDogAsync(); // Runs once at startup
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();