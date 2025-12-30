using Application.Cats.DTOs;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICatServiceApiClient
    {
        Task<IEnumerable<BreedDto>> GetBreedsAsync();
        Task<BreedDto?> GetBreedByIdAsync(string breedId);
        Task<IEnumerable<BreedDto>> SearchBreedsAsync(string query);
        Task<IEnumerable<CatsImageDTO>> GetImagesByBreedIdAsync(string image);
    }
}