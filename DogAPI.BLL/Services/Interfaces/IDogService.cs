using DogAPI.Common.DTOs;

namespace DogAPI.BLL.Services
{
    public interface IDogService
    {
        Task<List<DogDTO>> GetDogsAsync(GetDogsRequest? request);
        Task<DogDTO> AddDogAsync(CreateDogRequestDTO dogDTO);
        Task DeleteDogAsync(string name);
        Task<DogDTO> UpdateDogAsync(string name, UpdateDogRequestDTO dogDTO);
        Task<DogDTO> GetDogByNameAsync(string name);
    }
}
