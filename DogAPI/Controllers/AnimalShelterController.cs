using DogAPI.BLL.Services.Interfaces;
using DogAPI.Common.DTOs;
using DogAPI.DAL.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalShelterController : ControllerBase
    {
        private readonly IAnimalShelterService _animalShelterService;

        public AnimalShelterController(IAnimalShelterService animalShelterService)
        {
            _animalShelterService = animalShelterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimalShelterAsync([FromBody] CreateAnimalShelterDTO request)
        {
            var dogs = await _animalShelterService.CreateShelterAsync(request);
            return Ok(dogs);
        }
    }
}
