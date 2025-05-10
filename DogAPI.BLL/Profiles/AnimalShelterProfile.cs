using AutoMapper;
using DogAPI.Common.DTOs;
using DogAPI.DAL.Entities;

namespace DogAPI.BLL.Profiles
{
    public class AnimalShelterProfile : Profile
    {
        public AnimalShelterProfile()
        {
            CreateMap<AnimalShelter, AnimalShelterDTO>();

            CreateMap<CreateAnimalShelterDTO,  AnimalShelter>();
        }
    }
}
