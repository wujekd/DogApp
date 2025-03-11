using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

public sealed class DogApisTest : IAsyncLifetime
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _output;

    public DogApisTest(ITestOutputHelper output)
    {
        _httpClient = new HttpClient { BaseAddress = new System.Uri("http://localhost:5010/") };
        _output = output;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        _httpClient.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task GetAllDogs_ShouldReturnRecords()
    {
        // Act
        var response = await _httpClient.GetAsync("dogs/all");

        // Assert
        Assert.True(response.IsSuccessStatusCode, "API failed");
        var responseBody = await response.Content.ReadAsStringAsync();
        var dogs = JsonSerializer.Deserialize<DogsResponse[]>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        //Add some info to the output
        _output.WriteLine($"Number of dogs returned: {dogs.Length}");

        Assert.NotNull(dogs);
        Assert.NotEmpty(dogs);
    }
}

public class DogsResponse
{
    public int Id { get; set; }
    public string ImageSrc { get; set; }
    public int BreedId { get; set; }
}