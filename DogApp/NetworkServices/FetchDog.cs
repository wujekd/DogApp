namespace DogApp.NetworkServices;

public class FetchDog : IFetchDog
{
    
    private readonly HttpClient _httpClient;
    private const string DogApiUrl = "https://dog.ceo/api/breeds/image/random";

    public FetchDog(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> FetchDogFromApi()
    {
        var response = await _httpClient.GetAsync(DogApiUrl);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var json = await response.Content.ReadFromJsonAsync<FetchDogResponseDTO>();
        return json?.Message;
    }
}