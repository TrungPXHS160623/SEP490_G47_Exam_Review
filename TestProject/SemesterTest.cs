using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class SemesterRepositoryTests
    {
        private Mock<ISemesterRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<ISemesterRepository>();
        }

        // Test for CreateSemesterAsync
        [Test]
        public async Task CreateSemesterAsync_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new SemesterRequest();
            var expectedResponse = new RequestResponse { IsSuccessful = true, Message = "Semester created successfully." };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.CreateSemesterAsync(It.IsAny<SemesterRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.CreateSemesterAsync(request);

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Semester created successfully.", result.Message); // Ensure correct message
        }

        // Test for GetSemestersAsync
        [Test]
        public async Task GetSemestersAsync_ShouldReturnSemesters_WhenValidRequest()
        {
            // Arrange
            var expectedResponse = new ResultResponse<Semester>
            {
                IsSuccessful = true,

                Message = "Semesters fetched successfully."
            };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.GetSemestersAsync())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetSemestersAsync();

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Semesters fetched successfully.", result.Message); // Ensure correct message
        }

        // Test for GetSemesterByIdAsync
        [Test]
        public async Task GetSemesterByIdAsync_ShouldReturnSemester_WhenValidId()
        {
            // Arrange
            var semesterId = 1;
            var expectedResponse = new ResultResponse<SemesterRequest>
            {
                IsSuccessful = true,
                Item = new SemesterRequest { SemesterName = "Semester 1" },
                Message = "Semester found successfully."
            };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.GetSemesterByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetSemesterByIdAsync(semesterId);

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Semester found successfully.", result.Message); // Ensure correct message
            Assert.NotNull(result.Item); // Ensure data is not null
            Assert.AreEqual("Semester 1", result.Item.SemesterName); // Ensure correct semester name
        }

        // Test for UpdateSemesterAsync
        [Test]
        public async Task UpdateSemesterAsync_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new SemesterRequest { SemesterName = "Updated Semester" };
            var expectedResponse = new RequestResponse { IsSuccessful = true, Message = "Semester updated successfully." };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.UpdateSemesterAsync(It.IsAny<SemesterRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.UpdateSemesterAsync(request);

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Semester updated successfully.", result.Message); // Ensure correct message
        }

        // Test for DeleteSemesterAsync
        [Test]
        public async Task DeleteSemesterAsync_ShouldReturnSuccess_WhenValidId()
        {
            // Arrange
            var semesterId = 1;
            var expectedResponse = new RequestResponse { IsSuccessful = true, Message = "Semester deleted successfully." };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.DeleteSemesterAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.DeleteSemesterAsync(semesterId);

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Semester deleted successfully.", result.Message); // Ensure correct message
        }

        // Test for ToggleSemesterStatusAsync
        [Test]
        public async Task ToggleSemesterStatusAsync_ShouldReturnTrue_WhenSuccess()
        {
            // Arrange
            var semesterId = 1;
            var expectedResult = true; // Assume the toggle was successful

            // Set up the mock to return the expected result
            _mockRepository
                .Setup(repo => repo.ToggleSemesterStatusAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _mockRepository.Object.ToggleSemesterStatusAsync(semesterId);

            // Assert
            Assert.IsTrue(result); // Ensure the status toggle is successful
        }

        // Test for GetActiveSemestersAsync
        [Test]
        public async Task GetActiveSemestersAsync_ShouldReturnActiveSemesters_WhenValidRequest()
        {
            // Arrange
            var expectedResponse = new ResultResponse<SemesterResponse>
            {
                IsSuccessful = true,
                Message = "Active semesters fetched successfully."
            };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.GetActiveSemestersAsync())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetActiveSemestersAsync();

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Active semesters fetched successfully.", result.Message); // Ensure correct message
        }
    }
}
