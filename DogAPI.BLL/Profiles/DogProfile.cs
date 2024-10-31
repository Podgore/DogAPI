using AutoMapper;
using DogAPI.Common.DTOs;
using DogAPI.DAL.Entities;

namespace DogAPI.BLL.Profiles
{
    public class DogProfile : Profile
    {
        public DogProfile() 
        {
            CreateMap<Dog, DogDTO>();

            CreateMap<CreateDogDTO, Dog>();

            CreateMap<UpdateDogDTO, Dog>();

            CreateMap<Dog, UpdateDogDTO>();
        }
    }
}
