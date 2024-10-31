namespace DogAPI.Common.DTOs
{
    public class GetDogsRequest
    {
        public string? Atrribute { get; set; } = null!;

        public string? Order { get; set; } = null!;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
