using DogApp.Models;

namespace DogApp.NetworkServices;


    public interface IFetchDog
    {
        Task<string?> FetchDogFromApi();
    }