using AutoMapper;
using DogAPI.Common.DTOs;
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
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .OrderByAttribute(request.Atrribute ?? "Name", request.Order ?? "asc")
                    .ToListAsync();

            return _mapper.Map<List<DogDTO>>(sortedDogs);
        }

        public async Task<DogDTO> GetDogByName(string name)
        {
            var entity = await _dogsRepository.AsQueryable().FirstOrDefaultAsync(i => i.Name == name)
                ?? throw new Exception($"Unable to find entity with such a key: {name}");

            return _mapper.Map<DogDTO>(entity);
        }

        public async Task<DogDTO> AddDogAsync(DogDTO dogDTO)
        {
            if (await _dogsRepository.AsQueryable().FirstOrDefaultAsync(i => i.Name == dogDTO.Name) != null)
                throw new Exception($"Entity with key {dogDTO.Name} already exist in current database");

            var entity = _mapper.Map<Dog>(dogDTO);

            var result = await _dogsRepository.AddAsync(entity);

            return _mapper.Map<DogDTO>(entity);
        }

        public async Task<DogDTO> UpdateDogAsync(string name, UpdateDogDTO dogDTO)
        {
            var entity = await _dogsRepository.AsQueryable().FirstOrDefaultAsync(i => i.Name == name)
                ?? throw new Exception($"Unable to find entity with such a key: {name}");

            _mapper.Map(dogDTO, entity);

            var result = await _dogsRepository.UpdateAsync(entity);

            return _mapper.Map<DogDTO>(entity);
        }

        public async Task<bool> DeleteDog(string name)
        {
            var entity = await _dogsRepository.AsQueryable().FirstOrDefaultAsync(i => i.Name == name)
                ?? throw new Exception($"Unable to find entity with such a key: {name}");

            var res = await _dogsRepository.DeleteAsync(entity);

            return res > 0;
        }
    }
}
