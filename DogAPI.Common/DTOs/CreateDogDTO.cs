namespace DogAPI.Common.DTOs
{
    public class CreateDogDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public double TailLength { get; set; }
        public double Weight { get; set; }
        public Guid? AnimalShelterId { get; set; }
    }
}
