using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreedsController : ControllerBase
    {
        private readonly ICatServiceApiClient _BreedService;
        public BreedsController(ICatServiceApiClient breedService)
        {
            _BreedService = breedService;
        }


        /// <summary>
        /// Metodo para obtener lista de razas de gatos 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("breedsList")]
        public async Task<IActionResult> GetGatos()
        {
            try
            {
                var breeds = await _BreedService.GetBreedsAsync();
                return Ok(breeds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// metodo para obtener raza de gato por id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("breed_id")]
        public async Task<IActionResult> GetGatosId(string id)
        {
            try
            {
                var breeds = await _BreedService.GetBreedByIdAsync(id);
                return Ok(breeds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para buscar raza de gato por nombre razas 
        /// </summary>
        /// <param name="Search"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetGatosSearch(string Search)
        {
            try
            {
                var breeds = await _BreedService.SearchBreedsAsync(Search);
                return Ok(breeds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
