using DogAPI.BLL.Services;
using DogAPI.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogsController : ControllerBase
    {
        private readonly IDogService _dogService;

        public DogsController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetDogsAsync([FromQuery] GetDogsRequest request)
        {
            var dogs = await _dogService.GetDogsAsync(request);
            return Ok(dogs);
        }

        [HttpGet("dog/{name}")]
        public async Task<IActionResult> GetDogsByNameAsync(string name)
        {
            var dogs = await _dogService.GetDogByNameAsync(name);
            return Ok(dogs);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> CreateDogAsync(CreateDogDTO request)
        {
            var result = await _dogService.CreateDogAsync(request);
            return CreatedAtAction("dog", result);
        }

        [HttpDelete("dog/{name}")]
        public async Task<IActionResult> DeleteDogAsync(string name)
        {
            await _dogService.DeleteDogAsync(name);
            return NoContent();
        }

        [HttpPut("dog/{name}")]
        public async Task<IActionResult> UpdateDogAsync(string name, [FromQuery] UpdateDogDTO request)
        {
            var result = await _dogService.UpdateDogAsync(name, request);
            return Ok(result);
        }
    }
}
