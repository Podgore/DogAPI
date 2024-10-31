using Xunit;
using AutoMapper;
using DogAPI.BLL.Profiles;
using DogAPI.DAL.Repository.Interface;
using DogAPI.BLL.Services;
using DogAPI.Common.DTOs;
using NSubstitute;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute.ExceptionExtensions;
using DogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogAPI.Test
{
    public class DogServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IDogRepository _dogsRepository;
        private readonly IDogService _service;
        public DogServiceTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<DogProfile>()).CreateMapper();
            _dogsRepository = Substitute.For<IDogRepository>();

            _service = new DogService(_dogsRepository, _mapper);
        }

        [Fact]
        public async Task GetDogsAsync_WithPagination_ReturnsOnlyPageItems()
        {
            //Arrange
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();

            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            var request = new GetDogsRequest
            {
                PageSize = 10,
                Page = 1
            };

            var result = await _service.GetDogsAsync(request);

            result.Should()
                .NotBeNull()
                .And.HaveCount(10);
        }

        [Fact]
        public async Task GetDogsAsync_WithSortingAndPagination_ReturnsPagedAndSortedList()
        {
            //Arrange
            var dogs = DogTestData.GetMockDogs().AsQueryable();

            var dbSet = dogs.BuildMockDbSet();

            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            var request = new GetDogsRequest
            {
                Order = "asc",
                Atrribute = "Weight",
                PageSize = 10,
                Page = 1
            };

            var result = await _service.GetDogsAsync(request);

            var expectedList = dogs.OrderBy(d => d.Weight).Take(10);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(10)
                .And.NotBeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task GetDogsAsync_WithWrongAttribute_ThrowsException()
        {
            //Arrange
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();

            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            var request = new GetDogsRequest
            {
                Order = "asc",
                Atrribute = "Age",
            };

            Func<Task> result = async () => await _service.GetDogsAsync(request);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetDogByName_ValidName_ReturnsCorrectDog()
        {
            // Arrange
            var mockDogs = DogTestData.GetMockDogs().AsQueryable();
            var dbSet = mockDogs.AsQueryable().BuildMockDbSet();

            _dogsRepository.AsQueryable().Returns(dbSet);

            var existedDog = mockDogs.First();

            // Act
            var result = await _service.GetDogByName(existedDog.Name);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(existedDog.Name);
        }

        [Fact]
        public async Task GetDogByName_InvalidName_ThrowsException()
        {
            // Arrange
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            Func<Task> act = async () => await _service.GetDogByName("NonexistentEntity");

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Unable to find entity with such a key: NonexistentEntity");
        }

        [Fact]
        public async Task AddDogAsync_NewDog_AddsSuccessfully()
        {
            // Arrange
            var dogDTO = new DogDTO { Name = "NewDogEntity", Color = "grey", TailLenght = 23, Weight = 14};

            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();

            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            var result = await _service.AddDogAsync(dogDTO);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("NewDogEntity");
        }

        [Fact]
        public async Task AddDogAsync_DuplicateDog_ThrowsException()
        {
            // Arrange
            var mockDogs = DogTestData.GetMockDogs().AsQueryable();

            var dbSet = mockDogs.BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            var existedDog = mockDogs.First();

            var dogDTO = new DogDTO { Name = existedDog.Name , Color = "grey", TailLenght = 23, Weight = 14 };

            // Act
            Func<Task> act = async () => await _service.AddDogAsync(dogDTO);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage($"Entity with key {existedDog.Name} already exist in current database");
        }

        [Fact]
        public async Task UpdateDogAsync_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            var dogDTO = new UpdateDogDTO { Color = "yellow", TailLenght = 100, Weight = 100 };

            var mockDogs = DogTestData.GetMockDogs().AsQueryable();
            var dbSet = mockDogs.AsQueryable().BuildMockDbSet();

            _dogsRepository.AsQueryable().Returns(dbSet);

            var existedDog = mockDogs.First();

            // Act
            var result = await _service.UpdateDogAsync(existedDog.Name, dogDTO);

            // Assert
            result.Should().NotBeNull();
            result.Color.Should().Be("yellow");
            result.Weight.Should().Be(100);
            result.TailLenght.Should().Be(100);
        }

        [Fact]
        public async Task UpdateDogAsync_InvalidName_ThrowsException()
        {
            // Arrange
            var dogDTO = new UpdateDogDTO { Color = "yellow", TailLenght = 100, Weight = 100 };
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            Func<Task> act = async () => await _service.UpdateDogAsync("NonexistentEntity", dogDTO);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Unable to find entity with such a key: NonexistentEntity");
        }

        [Fact]
        public async Task DeleteDog_InvalidName_ThrowsException()
        {
            // Arrange
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            Func<Task> act = async () => await _service.DeleteDog("NonexistentEntity");

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Unable to find entity with such a key: NonexistentEntity");
        }

    }
}
