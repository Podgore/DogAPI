using DogAPI.DAL.DBContext;
using DogAPI.DAL.Entities;
using DogAPI.DAL.Repository.Base;
using DogAPI.DAL.Repository.Interface;

namespace DogAPI.DAL.Repository
{
    public class AnimalShelterRepository : RepositoryBase<AnimalShelter, Guid>, IAnimalShelterRepository
    {
        public AnimalShelterRepository(ApplicationDbContext context)
            : base(context) { }
    }
}
