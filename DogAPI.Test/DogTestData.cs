using Bogus;
using DogAPI.DAL.Entities;

namespace DogAPI.Test
{
    public class DogTestData
    {

        public static List<Dog> GetMockDogs()
        {
            string[] colors = ["black", "white", "brown", "ginger"];

            return new Faker<Dog>()
                .StrictMode(true)
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Color, f => f.PickRandom(colors))
                .RuleFor(x => x.TailLenght, f => f.Random.Number(5, 30))
                .RuleFor(x => x.Weight, f => f.Random.Number(3, 10))
                .Generate(20);
        }
    }
}


