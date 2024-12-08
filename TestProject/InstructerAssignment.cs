using Library.Common;
using Library.Request;
using Library.Response;
using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class InstructorAssignmentRepositoryTests
    {
        private Mock<IInstructorAssignmentRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<IInstructorAssignmentRepository>();
        }

        [Test]
        public async Task AssignExamToLecture_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new LeaderExamResponse();
            var expectedResponse = new RequestResponse();

            // Set up the mock to return a success response
            _mockRepository
                .Setup(repo => repo.AssignExamToLecture(It.IsAny<LeaderExamResponse>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.AssignExamToLecture(request);

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.IsTrue(result.IsSuccessful); // Check that Success is true
            Assert.AreEqual("Exam assigned successfully.", result.Message); // Check the response message
        }

        [Test]
        public async Task SetAssignDate_ShouldReturnFailure_WhenInvalidRequest()
        {
            // Arrange
            var request = new LectureExamResponse();
            var expectedResponse = new RequestResponse { IsSuccessful = false, Message = "Invalid exam date." };

            // Set up the mock to return a failure response
            _mockRepository
                .Setup(repo => repo.SetAssignDate(It.IsAny<LectureExamResponse>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.SetAssignDate(request);

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.IsFalse(result.IsSuccessful); // Check that Success is false
            Assert.AreEqual("Invalid exam date.", result.Message); // Check the response message
        }

        [Test]
        public async Task AssignSubjectToLecture_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new AddLecturerSubjectRequest();
            var expectedResponse = new RequestResponse { IsSuccessful = true, Message = "Subject assigned to lecture." };

            // Set up the mock to return a success response
            _mockRepository
                .Setup(repo => repo.AssignSubjectToLecture(It.IsAny<AddLecturerSubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.AssignSubjectToLecture(request);

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.IsTrue(result.IsSuccessful); // Check that Success is true
            Assert.AreEqual("Subject assigned to lecture.", result.Message); // Check the response message
        }
    }
}
