namespace DogAPI.Common.DTOs
{
    public class AnimalShelterDTO
    {
        public Guid Id { get; set; }

        public string Address { get; set; } = string.Empty;

        public int NumberOfAnimals { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
