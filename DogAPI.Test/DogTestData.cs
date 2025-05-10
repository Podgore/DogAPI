using Bogus;
using DogAPI.DAL.Entities;

namespace DogAPI.Test
{
    public class DogTestData
    {
        public static List<Dog> GetMockDogs()
        {
            string[] colors = ["black", "white", "brown", "ginger"];

            var animalShelterFaker = new Faker<AnimalShelter>()
                .StrictMode(true)
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Name, f => f.Company.CompanyName())
                .RuleFor(x => x.Address, f => f.Address.City())
                .RuleFor(x => x.NumberOfAnimals, f => f.Random.Number(5, 100))
                .RuleFor(x => x.CreationTime, f => f.Date.Past(10))
                .RuleFor(x => x.Dogs, _ => new List<Dog>());

            var dogFaker = new Faker<Dog>()
                .StrictMode(true)
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Color, f => f.PickRandom(colors))
                .RuleFor(x => x.TailLength, f => f.Random.Double(5, 30))
                .RuleFor(x => x.Weight, f => f.Random.Double(3, 10))
                .RuleFor(x => x.isDeleted, f => f.Random.Bool(0.1f))
                .RuleFor(x => x.DeletedAt, (f, d) => d.isDeleted ? f.Date.Past(1) : DateTime.MinValue)
                .RuleFor(x => x.AnimalShelter, f => animalShelterFaker.Generate())
                .RuleFor(x => x.AnimalShelterId, (f, d) => d.AnimalShelter.Id);

            return dogFaker.Generate(20);
        }
    }
}
