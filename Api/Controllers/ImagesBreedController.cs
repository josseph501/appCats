using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesBreedController : ControllerBase
    {
        private readonly ICatServiceApiClient _BreedService;
        public ImagesBreedController(ICatServiceApiClient breedService)
        {
            _BreedService = breedService;
        }

        /// <summary>
        /// Metodo para obtener imagenes de raza gatos por id   
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("imagesbybreedid")]
        public async Task<IActionResult> imagesbybreedid(string id)
        {
            try
            {
                var breeds = await _BreedService.GetImagesByBreedIdAsync(id);
                return Ok(breeds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

