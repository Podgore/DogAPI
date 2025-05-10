using System.Text.Json.Serialization;

namespace DogAPI.Common.DTOs
{
    public class CreateAnimalShelterDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int NumberOfAnimals { get; set; }

        [JsonIgnore]
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
