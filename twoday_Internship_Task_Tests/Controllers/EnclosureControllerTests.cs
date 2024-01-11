using FluentAssertions;
using Moq;
using twoday_Internship_Task.Controllers;
using twoday_Internship_Task.DtoModels;
using twoday_Internship_Task.Models;
using twoday_Internship_Task.Services.Interfaces;
using Xunit;

namespace twoday_Internship_Task_Tests.Controllers
{
    public class EnclosureControllerTests
    {
        private readonly Mock<IEnclosureService> _enclosureService;
        private readonly EnclosuresController _controller;

        public EnclosureControllerTests()
        {
            _enclosureService = new Mock<IEnclosureService>();
            _controller = new EnclosuresController(_enclosureService.Object);
        }

        [Fact]
        public async Task AddEnclosuresAsyncCalled_ReturnsExpectedEnclosureList()
        {
            // Arrange
            var enclosureData = new EnclosuresJsonModel();
            var expectedResult = new List<EnclosureGETModel>();
            _enclosureService.Setup(x => x.AddEnclosuresAsync(enclosureData)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.AddEnclosuresAsync(enclosureData);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAllEnclosuresAsyncCalled_ReturnsExpectedEnclosureList()
        {
            // Arrange
            var expectedResult = new List<EnclosureGETModel>();
            _enclosureService.Setup(x => x.GetAllEnclosuresAsync()).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetAllEnclosuresAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task DeleteAnimalsAsyncCalled_CallsDeleteEnclosureAsyncService()
        {
            // Arrange
            var name = " Name 1  ";

            // Act
            await _controller.DeleteEnclosureAsync(name);

            // Assert
            _enclosureService.Verify(x => x.DeleteEnclosureAsync(name.Trim().ToLower()), Times.Once);
        }
    }
}
