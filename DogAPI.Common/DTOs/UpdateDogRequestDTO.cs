﻿namespace DogAPI.Common.DTOs
{
    public class UpdateDogRequestDTO
    {
        public string Color { get; set; } = string.Empty;
        public double TailLenght { get; set; }
        public double Weight { get; set; }
    }
}