using Domain.Contracts;
using Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Infrastructure.Clients
{
    public class CatApiClientRepository : ICatApiClientRepository
    {
        private readonly HttpClient _httpClient;

        public CatApiClientRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CatBreed>> GetBreedsAsync()
        {
            var response = await _httpClient
                .GetFromJsonAsync<List<CatApiBreedResponse>>("breeds");

            return response!.Select(MapToDomain);
        }

        public async Task<CatBreed?> GetBreedByIdAsync(string breedId)
        {
            var response = await _httpClient
                .GetFromJsonAsync<CatApiBreedResponse>($"breeds/{breedId}");

            return response is null ? null : MapToDomain(response);
        }

        public async Task<IEnumerable<CatBreed>> SearchBreedsAsync(string query)
        {
            var response = await _httpClient
                .GetFromJsonAsync<List<CatApiBreedResponse>>($"breeds/search?q={query}");

            return response!.Select(MapToDomain);
        }


        public async Task<IEnumerable<CatsImage>> GetImagesByBreedIdAsync(string searh)
        {

            var response = await _httpClient
              .GetFromJsonAsync<List<CatApImageResponse>>($"images/search?{searh}");

            return response!.Select(MapToDomain2);

        }


        private static CatBreed MapToDomain(CatApiBreedResponse breed)
        {
            return new CatBreed
            {
                Id = breed.Id,
                Name = breed.Name,
                Origin = breed.Origin,
                Temperament = breed.Temperament,
                Description = breed.Description,
                LifeSpan = breed.LifeSpan,
                ImageId = breed.ReferenceImageId
            };
        }


        private static CatsImage MapToDomain2(CatApImageResponse image)
        {
            return new CatsImage
            {
                id = image.id,
                url = image.url,
                width = image.width,
                height = image.height

            };
        }
    }
}