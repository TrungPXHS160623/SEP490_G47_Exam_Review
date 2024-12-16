using Library.Common;
using Library.Models;
using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class RoleRepositoryTests
    {
        private Mock<IRoleRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<IRoleRepository>();
        }

        // Test for GetRolesForAdmin
        [Test]
        public async Task GetRolesForAdmin_ShouldReturnRoles_WhenValidRequest()
        {
            // Arrange
            var expectedRoles = new UserRole[] {
                new UserRole { RoleId = 1, RoleName = "Admin" },
                new UserRole { RoleId = 2, RoleName = "SuperAdmin" }
            };
            var expectedResponse = new ResultResponse<UserRole>
            {
                IsSuccessful = true,
                Message = "Roles for Admin fetched successfully."
            };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.GetRolesForAdmin())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetRolesForAdmin();

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Roles for Admin fetched successfully.", result.Message); // Ensure correct message
        }

        // Test for GetRolesForExaminer
        [Test]
        public async Task GetRolesForExaminer_ShouldReturnRoles_WhenValidRequest()
        {
            // Arrange
            var expectedRoles = new UserRole[] {
                new UserRole { RoleId = 1, RoleName = "Examiner" }
            };
            var expectedResponse = new ResultResponse<UserRole>
            {
                IsSuccessful = true,
                Message = "Roles for Examiner fetched successfully."
            };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.GetRolesForExaminer())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetRolesForExaminer();

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Roles for Examiner fetched successfully.", result.Message); // Ensure correct message
        }

        // Test for GetRolesForAdmin when no roles exist
        [Test]
        public async Task GetRolesForAdmin_ShouldReturnEmpty_WhenNoRolesExist()
        {
            // Arrange
            var expectedResponse = new ResultResponse<UserRole>
            {
                IsSuccessful = true,
                Message = "No roles found for Admin."
            };

            // Set up the mock to return the empty response
            _mockRepository
                .Setup(repo => repo.GetRolesForAdmin())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetRolesForAdmin();

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
        }

        // Test for GetRolesForExaminer when no roles exist
        [Test]
        public async Task GetRolesForExaminer_ShouldReturnEmpty_WhenNoRolesExist()
        {
            // Arrange
            var expectedResponse = new ResultResponse<UserRole>
            {
                IsSuccessful = true,
                Message = "No roles found for Examiner."
            };

            // Set up the mock to return the empty response
            _mockRepository
                .Setup(repo => repo.GetRolesForExaminer())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetRolesForExaminer();

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true

        }
    }
}
