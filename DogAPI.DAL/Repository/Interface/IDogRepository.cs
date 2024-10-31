using DogAPI.DAL.Entities;
using DogAPI.DAL.Repository.Base;

namespace DogAPI.DAL.Repository.Interface
{
    public interface IDogRepository : IRepository<Dog, string>
    {
    }
}
