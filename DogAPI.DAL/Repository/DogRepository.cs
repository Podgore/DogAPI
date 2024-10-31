using DogAPI.DAL.DBContext;
using DogAPI.DAL.Entities;
using DogAPI.DAL.Repository.Base;
using DogAPI.DAL.Repository.Interface;

namespace DogAPI.DAL.Repository
{
    public class DogRepository : RepositoryBase<Dog, string>, IDogRepository
    {
        public DogRepository(ApplicationDbContext context) : base(context) { }
    }
}
