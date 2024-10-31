using Xunit;
using AutoMapper;
using DogAPI.BLL.Profiles;
using DogAPI.DAL.Repository.Interface;
using DogAPI.BLL.Services;
using DogAPI.Common.DTOs;
using NSubstitute;
using FluentAssertions;
using MockQueryable.NSubstitute;
using DogAPI.Common.Exceptions;

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
                .And.BeEquivalentTo(expectedList);
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
            await result.Should().ThrowAsync<NotFoundException>();
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
            var result = await _service.GetDogByNameAsync(existedDog.Name);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(existedDog.Name);
        }

        [Theory]
        [InlineData("NonexistentEntity")]
        public async Task GetDogByName_InvalidName_ThrowsException(string invalidName)
        {
            // Arrange
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            Func<Task> act = async () => await _service.GetDogByNameAsync(invalidName);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Unable to find entity with such a key: {invalidName}");
        }

        [Fact]
        public async Task AddDogAsync_NewDog_AddsSuccessfully()
        {
            // Arrange
            var dogDTO = new CreateDogDTO { Name = "NewDogEntity", Color = "grey", TailLenght = 23, Weight = 14};

            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();

            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            var result = await _service.AddDogAsync(dogDTO);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dogDTO.Name);
        }

        [Fact]
        public async Task AddDogAsync_DuplicateDog_ThrowsException()
        {
            // Arrange
            var mockDogs = DogTestData.GetMockDogs().AsQueryable();

            var dbSet = mockDogs.BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            var existedDog = mockDogs.First();

            var dogDTO = new CreateDogDTO { Name = existedDog.Name , Color = "grey", TailLenght = 23, Weight = 14 };

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
            result.Color.Should().Be(dogDTO.Color);
            result.Weight.Should().Be(dogDTO.Weight);
            result.TailLenght.Should().Be(dogDTO.TailLenght);
        }

        [Theory]
        [InlineData("NonexistentEntity")]
        public async Task UpdateDogAsync_InvalidName_ThrowsException(string invalidName)
        {
            // Arrange
            var dogDTO = new UpdateDogDTO { Color = "yellow", TailLenght = 100, Weight = 100 };
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            Func<Task> act = async () => await _service.UpdateDogAsync(invalidName, dogDTO);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage($"Unable to find entity with such a key: {invalidName}");
        }

        [Theory]
        [InlineData("NonexistentEntity")]
        public async Task DeleteDog_InvalidName_ThrowsException(string invalidName)
        {
            // Arrange
            var dbSet = DogTestData.GetMockDogs().AsQueryable().BuildMockDbSet();
            _dogsRepository.AsQueryable().Returns(dbSet);

            // Act
            Func<Task> act = async () => await _service.DeleteDogAsync(invalidName);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage($"Unable to find entity with such a key: {invalidName}");
        }
    }
}
