using Application.Cats.DTOs;
using Application.Interfaces;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CatServiceClient : ICatServiceApiClient
    {
        private readonly ICatApiClientRepository _repository;
        public CatServiceClient(ICatApiClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<BreedDto?> GetBreedByIdAsync(string breedId)
        {

            try
            {
                var breeds = await _repository.GetBreedByIdAsync(breedId);

                if (breeds == null)
                {
                    return new();
                }

                var response = MapToDto(breeds);

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("error" + ex.Message);
            }
        }

        public async Task<IEnumerable<BreedDto>> GetBreedsAsync()
        {
            try
            {
                var breeds = await _repository.GetBreedsAsync();
                return breeds.Select(MapToDto);

            }
            catch (Exception ex)
            {
                throw new Exception("error" + ex.Message);
            }
        }

        public async Task<IEnumerable<BreedDto>> SearchBreedsAsync(string query)
        {
            try
            {
                var breeds = await _repository.SearchBreedsAsync(query);
                return breeds.Select(MapToDto);
            }
            catch (Exception ex)
            {

                throw new Exception("error" + ex.Message);
            }
        }

        public async Task<IEnumerable<CatsImageDTO>> GetImagesByBreedIdAsync(string image)
        {
            try
            {
                var breeds = await _repository.GetImagesByBreedIdAsync(image);
                return breeds.Select(MapToDto2);
            }
            catch (Exception ex)
            {

                throw new Exception("error" + ex.Message);
            }
        }

        private static BreedDto MapToDto(CatBreed breed)
        {
            return new BreedDto
            {
                Id = breed.Id,
                Name = breed.Name,
                Origin = breed.Origin,
                Temperament = breed.Temperament,
                Description = breed.Description,
                LifeSpan = breed.LifeSpan,
                ImageId = breed.ImageId
            };
        }


        private static CatsImageDTO MapToDto2(CatsImage image)
        {
            return new CatsImageDTO
            {
                id = image.id,
                url = image.url,
                width = image.width,
                height = image.height
            };
        }
    }
}
