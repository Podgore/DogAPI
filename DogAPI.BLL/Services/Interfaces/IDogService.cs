using DogAPI.Common.DTOs;

namespace DogAPI.BLL.Services
{
    public interface IDogService
    {
        public Task<List<DogDTO>> GetDogsAsync(GetDogsRequest? request);
        public Task<DogDTO> AddDogAsync(DogDTO dogDTO);
        public Task<bool> DeleteDog(string name);
        public Task<DogDTO> UpdateDogAsync(string name, UpdateDogDTO dogDTO);
        Task<DogDTO> GetDogByName(string name);
    }
}
