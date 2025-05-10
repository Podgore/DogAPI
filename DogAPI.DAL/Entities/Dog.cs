using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogAPI.DAL.Entities
{
    public class Dog
    {
        [Key]
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public double TailLength { get; set; }

        public double Weight { get; set; }

        public bool isDeleted { get; set; }

        public DateTime DeletedAt { get; set; }

        [ForeignKey(nameof(AnimalShelter))]
        public Guid? AnimalShelterId { get; set; }

        public AnimalShelter AnimalShelter { get; set; } = null!;
    }
}
