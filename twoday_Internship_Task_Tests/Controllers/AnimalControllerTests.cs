using FluentAssertions;
using Moq;
using twoday_Internship_Task.Controllers;
using twoday_Internship_Task.Models;
using twoday_Internship_Task.Services.Interfaces;
using Xunit;

namespace twoday_Internship_Task_Tests.Controllers
{
    public class AnimalControllerTests
    {
        private readonly Mock<IAnimalsService> _animalsService;
        private readonly AnimalsController _controller;

        public AnimalControllerTests()
        {
            _animalsService = new Mock<IAnimalsService>();
            _controller = new AnimalsController(_animalsService.Object);
        }

        [Fact]
        public async Task AddAnimalsAsyncCalled_ReturnsExpectedAnimalList()
        {
            // Arrange
            var animalData = new AnimalsJsonModel();
            var expectedAnimalList = new List<AnimalModel>();

            _animalsService.Setup(x => x.AddAnimalsAsync(animalData)).ReturnsAsync(expectedAnimalList);

            // Act
            var result = await _controller.AddAnimalsAsync(animalData);

            // Assert
            result.Should().BeEquivalentTo(expectedAnimalList);
        }

        [Fact]
        public async Task DeleteAnimalsAsyncCalled_CallsDeleteAnimalsAsyncService()
        {
            // Arrange
            var species = " String  ";
            var amount = 2;

            // Act
            await _controller.DeleteAnimalsAsync(species, amount);

            // Assert
            _animalsService.Verify(x => x.DeleteAnimalsAsync(species.ToLower().Trim(), amount), Times.Once);
        }
    }
}