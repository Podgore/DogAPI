namespace DogAPI.Common.DTOs
{
    public class DogDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public double TailLength { get; set; }
        public double Weight { get; set; }
        public AnimalShelterDTO AnimalShelter { get; set; } = null!;
    }
}
