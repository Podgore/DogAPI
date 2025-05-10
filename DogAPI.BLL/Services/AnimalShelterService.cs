using AutoMapper;
using DogAPI.BLL.Services.Interfaces;
using DogAPI.Common.DTOs;
using DogAPI.DAL.Entities;
using DogAPI.DAL.Repository.Interface;

namespace DogAPI.BLL.Services
{
    public class AnimalShelterService : IAnimalShelterService
    {
        private readonly IAnimalShelterRepository _animalShelterRepository;
        private readonly IMapper _mapper;

        public AnimalShelterService(IAnimalShelterRepository animalShelterRepository, IMapper mapper)
        {
            _animalShelterRepository = animalShelterRepository;
            _mapper = mapper;
        }

        public async Task<AnimalShelterDTO> CreateShelterAsync(CreateAnimalShelterDTO request)
        {
            var animalShelter = _mapper.Map<AnimalShelter>(request);

            await _animalShelterRepository.AddAsync(animalShelter);

            return _mapper.Map<AnimalShelterDTO>(animalShelter);
        }
    }
}
