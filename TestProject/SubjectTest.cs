using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class SubjectRepositoryTests
    {
        private Mock<ISubjectRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<ISubjectRepository>();
        }

        // Test for GetSubjects
        [Test]
        public async Task GetSubjects_ShouldReturnSubjects_WhenValidRequest()
        {
            // Arrange
            var expectedResponse = new ResultResponse<Subject>
            {
                IsSuccessful = true,
                Message = "Subjects fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetSubjects())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetSubjects();

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subjects fetched successfully.", result.Message);
        }

        // Test for GetSubjectList
        [Test]
        public async Task GetSubjectList_ShouldReturnSubjectList_WhenValidRequest()
        {
            // Arrange
            var request = new SubjectRequest { FacultyId = 1 };
            var expectedResponse = new ResultResponse<SubjectResponse>
            {
                IsSuccessful = true,

                Message = "Subject list fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetSubjectList(It.IsAny<SubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetSubjectList(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subject list fetched successfully.", result.Message);
        }

        // Test for GetLectureSubjectList
        [Test]
        public async Task GetLectureSubjectList_ShouldReturnLectureSubjectList_WhenValidRequest()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new ResultResponse<SubjectResponse>
            {
                IsSuccessful = true,

                Message = "Lecture subjects fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetLectureSubjectList(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetLectureSubjectList(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Lecture subjects fetched successfully.", result.Message);
        }

        // Test for GetHeadSubjectList
        [Test]
        public async Task GetHeadSubjectList_ShouldReturnHeadSubjectList_WhenValidRequest()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new ResultResponse<HeadSubjectRepsonse>
            {
                IsSuccessful = true,

                Message = "Head subjects fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetHeadSubjectList(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetHeadSubjectList(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Head subjects fetched successfully.", result.Message);
        }

        // Test for GetSubjectById
        [Test]
        public async Task GetSubjectById_ShouldReturnSubject_WhenValidId()
        {
            // Arrange
            var subjectId = 1;
            var expectedResponse = new ResultResponse<SubjectRequest>
            {
                IsSuccessful = true,
                Item = new SubjectRequest { SubjectCode = "Mathematics", FacultyId = 1 },
                Message = "Subject found successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetSubjectById(subjectId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subject found successfully.", result.Message);
            Assert.NotNull(result.Item);
        }

        // Test for AddSubject
        [Test]
        public async Task AddSubject_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new SubjectRequest { SubjectCode = "Physics" };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Subject added successfully."
            };

            _mockRepository
                .Setup(repo => repo.AddSubject(It.IsAny<SubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.AddSubject(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subject added successfully.", result.Message);
        }

        // Test for UpdateSubject
        [Test]
        public async Task UpdateSubject_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new SubjectRequest { SubjectId = 1, SubjectName = "Updated Physics" };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Subject updated successfully."
            };

            _mockRepository
                .Setup(repo => repo.UpdateSubject(It.IsAny<SubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.UpdateSubject(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subject updated successfully.", result.Message);
        }

        // Test for DeleteSubject
        [Test]
        public async Task DeleteSubject_ShouldReturnSuccess_WhenValidId()
        {
            // Arrange
            var subjectId = 1;
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Subject deleted successfully."
            };

            _mockRepository
                .Setup(repo => repo.DeleteSubject(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.DeleteSubject(subjectId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subject deleted successfully.", result.Message);
        }

        // Test for AddSubjectToDepartment
        [Test]
        public async Task AddSubjectToDepartment_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new SubjectDepartmentRequest { SubjectId = 1, DepartmentId = 1 };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Subject added to department successfully."
            };

            _mockRepository
                .Setup(repo => repo.AddSubjectToDepartment(It.IsAny<SubjectDepartmentRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.AddSubjectToDepartment(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subject added to department successfully.", result.Message);
        }

        // Test for ImportSubjectsFromExcel
        [Test]
        public async Task ImportSubjectsFromExcel_ShouldReturnSuccess_WhenValidFile()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var claimsPrincipal = new ClaimsPrincipal();
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Subjects imported successfully."
            };

            _mockRepository
                .Setup(repo => repo.ImportSubjectsFromExcel(It.IsAny<IFormFile>(), It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.ImportSubjectsFromExcel(mockFile.Object, claimsPrincipal);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Subjects imported successfully.", result.Message);
        }
    }
}
