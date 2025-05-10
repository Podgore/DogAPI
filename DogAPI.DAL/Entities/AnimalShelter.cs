using System.ComponentModel.DataAnnotations;

namespace DogAPI.DAL.Entities
{
    public class AnimalShelter
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int NumberOfAnimals { get; set; }

        public DateTime CreationTime {  get; set; }

        public List<Dog> Dogs { get; set; } = new();
    }
}
