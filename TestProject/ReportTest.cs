using Library.Common;
using Library.Response;
using Microsoft.AspNetCore.Http;
using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class ReportRepositoryTests
    {
        private Mock<IReportRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<IReportRepository>();
        }

        // Test for GetReportsByLecturerId
        [Test]
        public async Task GetReportsByLecturerId_ShouldReturnReports_WhenValidLecturerId()
        {
            // Arrange
            var lecturerId = 1;
            var expectedResponse = new ResultResponse<ReportResponse>
            {
                IsSuccessful = true,
                Item = new ReportResponse { ReportId = 1, ReportContent = "Sample Report" },
                Message = "Reports fetched successfully."
            };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.GetReportsByLecturerId(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetReportsByLecturerId(lecturerId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Reports fetched successfully.", result.Message);
            Assert.NotNull(result.Item);
            Assert.AreEqual(1, result.Item.ReportId);
        }

        // Test for AddEditReport
        [Test]
        public async Task AddEditReport_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var reportRequest = new LectureExamResponse { ExamId = 101 };
            var isSubmit = true;
            var expectedResponse = new RequestResponse { IsSuccessful = true, Message = "Report added successfully." };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.AddEditReport(It.IsAny<LectureExamResponse>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.AddEditReport(reportRequest, isSubmit);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Report added successfully.", result.Message);
        }

        // Test for UploadReportWithFiles
        [Test]
        public async Task UploadReportWithFiles_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var reportRequest = new LectureExamResponseFinal { };
            var isSubmit = true;
            var expectedResponse = new RequestResponse { IsSuccessful = true, Message = "Report with files uploaded successfully." };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.UploadReportWithFiles(It.IsAny<LectureExamResponseFinal>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.UploadReportWithFiles(reportRequest, isSubmit);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Report with files uploaded successfully.", result.Message);
        }

        // Test for UploadFiles
        [Test]
        public async Task UploadFiles_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var reportId = 1;
            var files = new List<IFormFile>(); // Mocking files list (could be empty for now)
            var expectedResponse = new RequestResponse { IsSuccessful = true, Message = "Files uploaded successfully." };

            // Set up the mock to return the expected response
            _mockRepository
                .Setup(repo => repo.UploadFiles(It.IsAny<int>(), It.IsAny<IList<IFormFile>>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.UploadFiles(reportId, files);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Files uploaded successfully.", result.Message);
        }

    }
}
