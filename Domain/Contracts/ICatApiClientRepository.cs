using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICatApiClientRepository
    {
        Task<IEnumerable<CatBreed>> GetBreedsAsync();
        Task<CatBreed?> GetBreedByIdAsync(string breedId);
        Task<IEnumerable<CatBreed>> SearchBreedsAsync(string query);
        Task<IEnumerable<CatsImage>> GetImagesByBreedIdAsync(string image);
    }
}
