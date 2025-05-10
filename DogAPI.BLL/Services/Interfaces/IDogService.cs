using DogAPI.Common.DTOs;

namespace DogAPI.BLL.Services
{
    public interface IDogService
    {
        Task<List<DogDTO>> GetDogsAsync(GetDogsRequest? request);
        Task<DogDTO> CreateDogAsync(CreateDogDTO dogDTO);
        Task DeleteDogAsync(string name);
        Task<DogDTO> UpdateDogAsync(string name, UpdateDogDTO dogDTO);
        Task<DogDTO> GetDogByNameAsync(string name);
    }
}
