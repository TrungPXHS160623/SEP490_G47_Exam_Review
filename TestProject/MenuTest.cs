using Library.Common;
using Library.Models;
using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class MenuTest
    {
        private Mock<IMenuRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<IMenuRepository>();
        }

        [Test]
        public async Task GetMenu_ShouldReturnMenu_WhenValidRoleId()
        {
            // Arrange
            var roleId = 1;  // Example roleId
            var expectedMenu = new Menu { MenuId = 1, MenuLink = "" };  // Example Menu object
            var expectedResponse = new ResultResponse<Menu>
            {
                IsSuccessful = true,
                Item = expectedMenu,
                Message = "Menu fetched successfully."
            };

            // Set up the mock to return a valid menu response for the given roleId
            _mockRepository
                .Setup(repo => repo.GetMenu(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetMenu(roleId);

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure the success flag is true
            Assert.AreEqual("Menu fetched successfully.", result.Message); // Ensure the success message is correct
        }

        [Test]
        public async Task GetMenu_ShouldReturnFailure_WhenInvalidRoleId()
        {
            // Arrange
            var roleId = -1;  // Example invalid roleId
            var expectedResponse = new ResultResponse<Menu>
            {
                IsSuccessful = true,
                Message = "Invalid RoleID."
            };

            // Set up the mock to return a failure response for an invalid roleId
            _mockRepository
                .Setup(repo => repo.GetMenu(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetMenu(roleId);

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.IsFalse(result.IsSuccessful); // Ensure the success flag is false
            Assert.AreEqual("Invalid role ID.", result.Message); // Ensure the failure message is correct
            Assert.IsNull(result.Item); // Ensure that the Data (Menu) is null
        }
    }
}
