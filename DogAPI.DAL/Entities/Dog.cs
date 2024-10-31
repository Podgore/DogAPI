using System.ComponentModel.DataAnnotations;

namespace DogAPI.DAL.Entities
{
    public class Dog
    {
        [Key]
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public double TailLenght { get; set; }
        public double Weight { get; set; }
    }
}
