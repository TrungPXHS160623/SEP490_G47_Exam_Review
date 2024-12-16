using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class FacultyRepositoryTests
    {
        private Mock<IFacultyRepository> _mockFacultyRepository;

        [SetUp]
        public void SetUp()
        {
            _mockFacultyRepository = new Mock<IFacultyRepository>();
        }

        [Test]
        public async Task GetFaculties_ShouldReturnFacultyList()
        {
            // Arrange
            var faculties = new List<Faculty>();

            var expectedResponse = new ResultResponse<Faculty>
            {
                IsSuccessful = true,
                Items = faculties
            };

            _mockFacultyRepository.Setup(repo => repo.GetFaculties())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockFacultyRepository.Object.GetFaculties();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(2, result.Items.Count);
        }

        [Test]
        public async Task DeleteFaculties_ShouldReturnSuccessResponse()
        {
            // Arrange
            int facultyId = 1;

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Faculty deleted successfully."
            };

            _mockFacultyRepository.Setup(repo => repo.DeleteFaculties(facultyId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockFacultyRepository.Object.DeleteFaculties(facultyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Faculty deleted successfully.", result.Message);
        }

        [Test]
        public async Task GetHeadFaculties_ShouldReturnHeadFacultyList()
        {
            // Arrange
            int userId = 1;

            var headFaculties = new List<Faculty>();

            var expectedResponse = new ResultResponse<Faculty>
            {
                IsSuccessful = true,
                Items = headFaculties
            };

            _mockFacultyRepository.Setup(repo => repo.GetHeadFaculties(userId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockFacultyRepository.Object.GetHeadFaculties(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(1, result.Items.Count);
        }

        [Test]
        public async Task GetFacultiesByUserId_ShouldReturnFacultyRequestList()
        {
            // Arrange
            int? userId = 1;

            var faculties = new List<FacutyRequest>();

            var expectedResponse = new ResultResponse<FacutyRequest>
            {
                IsSuccessful = true,
                Items = faculties
            };

            _mockFacultyRepository.Setup(repo => repo.GetFacutiesByUserID(userId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockFacultyRepository.Object.GetFacutiesByUserID(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(2, result.Items.Count);
        }

        [Test]
        public async Task GetFacultyByRole_ShouldReturnFacultyResponseList()
        {
            // Arrange
            int roleId = 1;
            int userId = 1;
            int campusId = 1;

            var faculties = new List<FacutyResponse>();

            var expectedResponse = new ResultResponse<FacutyResponse>
            {
                IsSuccessful = true,
                Items = faculties
            };

            _mockFacultyRepository.Setup(repo => repo.GetFacutyByRole(roleId, userId, campusId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockFacultyRepository.Object.GetFacutyByRole(roleId, userId, campusId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(2, result.Items.Count);
        }

        [Test]
        public async Task CreateFacultyAsync_ShouldReturnSuccessResponse()
        {
            // Arrange
            var request = new FacutyRequest();

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Faculty created successfully."
            };

            _mockFacultyRepository.Setup(repo => repo.CreateFacutyAsync(request))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockFacultyRepository.Object.CreateFacutyAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Faculty created successfully.", result.Message);
        }

        [Test]
        public async Task UpdateFacultyAsync_ShouldReturnSuccessResponse()
        {
            // Arrange
            var request = new FacutyRequest();

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Faculty updated successfully."
            };

            _mockFacultyRepository.Setup(repo => repo.UpdateFacutyAsync(request))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockFacultyRepository.Object.UpdateFacutyAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Faculty updated successfully.", result.Message);
        }
    }
}
