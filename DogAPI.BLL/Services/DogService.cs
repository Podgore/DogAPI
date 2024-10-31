using AutoMapper;
using DogAPI.Common.DTOs;
using DogAPI.Common.Exceptions;
using DogAPI.DAL.Entities;
using DogAPI.DAL.Extensions;
using DogAPI.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DogAPI.BLL.Services
{
    public class DogService : IDogService
    {
        private readonly IDogRepository _dogsRepository;
        private readonly IMapper _mapper;

        public DogService(IDogRepository dogsRepository, IMapper mapper)
        {
            _dogsRepository = dogsRepository;
            _mapper = mapper;
        }

        public async Task<List<DogDTO>> GetDogsAsync(GetDogsRequest request)
        {
            var sortedDogs = await _dogsRepository.AsQueryable()
                    .OrderByAttribute(request.Atrribute ?? nameof(Dog.Name), request.Order!)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return _mapper.Map<List<DogDTO>>(sortedDogs);
        }

        public async Task<DogDTO> GetDogByNameAsync(string name)
        {
            var entity = await _dogsRepository.AsQueryable().FirstOrDefaultAsync(i => i.Name == name)
                ?? throw new NotFoundException($"Unable to find entity with such a key: {name}");

            return _mapper.Map<DogDTO>(entity);
        }

        public async Task<DogDTO> AddDogAsync(CreateDogRequestDTO dogDTO)
        {
            if (await _dogsRepository.AsQueryable().AnyAsync(i => i.Name == dogDTO.Name))
                throw new AlreadyExistsException($"Entity with key {dogDTO.Name} already exist in current database");

            var entity = _mapper.Map<Dog>(dogDTO);

            await _dogsRepository.AddAsync(entity);

            return _mapper.Map<DogDTO>(entity);
        }

        public async Task<DogDTO> UpdateDogAsync(string name, UpdateDogRequestDTO dogDTO)
        {
            var entity = await _dogsRepository.AsQueryable().FirstOrDefaultAsync(i => i.Name == name)
                ?? throw new NotFoundException($"Unable to find entity with such a key: {name}");

            _mapper.Map(dogDTO, entity);

            await _dogsRepository.UpdateAsync(entity);

            return _mapper.Map<DogDTO>(entity);
        }

        public async Task DeleteDogAsync(string name)
        {
            var entity = await _dogsRepository.AsQueryable().FirstOrDefaultAsync(i => i.Name == name)
                ?? throw new NotFoundException($"Unable to find entity with such a key: {name}");

            await _dogsRepository.DeleteAsync(entity);
        }
    }
}
