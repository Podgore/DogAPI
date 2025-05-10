using DogAPI.Common.DTOs;

namespace DogAPI.BLL.Services.Interfaces
{
    public interface IAnimalShelterService
    {
        Task<AnimalShelterDTO> CreateShelterAsync(CreateAnimalShelterDTO request);
    }
}
