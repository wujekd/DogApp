using System.Text;
using DogApp;
using DogApp.NetworkServices;
using Hangfire;
using Hangfire.Console;
using Hangfire.MySql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DM Web API's",
        Version = "v1"
    });

    var security = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    };

    x.AddSecurityDefinition("Bearer", security);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };
    x.AddSecurityRequirement(securityRequirement);
});


// Registers with DI to manage HttpClient (no multiple instances per request)
builder.Services.AddHttpClient<IFetchDog, FetchDog>();


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),

    };
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 0;
});



// database <-> ORM (Entity Framework) DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
        )
    );
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddHangfire(opts =>
{
    opts.UseStorage(new MySqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")+"Allow User Variables=true;",
        new MySqlStorageOptions() {TablesPrefix = "hangfire_"}));
   opts.UseColouredConsoleLogProvider();
});
builder.Services.AddHangfireServer();
GlobalConfiguration.Configuration.UseConsole().UseColouredConsoleLogProvider();

builder.Services.AddScoped<DogAdder>();

var app  = builder.Build();

// run on startup
using (var scope = app.Services.CreateScope())
{
    var dogManager = scope.ServiceProvider.GetRequiredService<DogAdder>();
    await dogManager.AddDogFromAPI();
}

//add the job to hangfire
using (var scope = app.Services.CreateScope())
{
    var recurringJobs = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobs.AddOrUpdate("DogsAdder",
        ()=> scope.ServiceProvider.GetRequiredService<DogAdder>().AddDogFromAPI(null),
        Cron.Minutely());
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    
app.UseAuthorization();
app.UseHangfireDashboard();
app.MapControllers();

app.Run();