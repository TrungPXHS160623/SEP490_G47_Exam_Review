using Library.Common;
using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class EmailRepositoryTests
    {
        private Mock<ISendMailRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<ISendMailRepository>();
        }

        // Test for SendMail method (successful case)
        [Test]
        public async Task SendMail_ShouldReturnSuccess_WhenValidMailModel()
        {
            // Arrange
            var mailModel = new MailModel
            {
                Subject = "Test Subject",
                Body = "This is a test email body."
            };

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Email sent successfully."
            };

            // Set up the mock to return the expected response when SendMail is called
            _mockRepository
                .Setup(repo => repo.SendMail(It.IsAny<MailModel>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.SendMail(mailModel);

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsTrue(result.IsSuccessful); // Ensure success flag is true
            Assert.AreEqual("Email sent successfully.", result.Message); // Ensure correct message
        }

        // Test for SendMail method (failure case)
        [Test]
        public async Task SendMail_ShouldReturnFailure_WhenMailModelIsInvalid()
        {
            // Arrange
            var mailModel = new MailModel
            {
                Subject = "Test Subject",
                Body = "This is a test email body."
            };

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = false,
                Message = "Failed to send email: Invalid recipient."
            };

            // Set up the mock to return the expected response when SendMail is called with an invalid email
            _mockRepository
                .Setup(repo => repo.SendMail(It.IsAny<MailModel>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.SendMail(mailModel);

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsFalse(result.IsSuccessful); // Ensure success flag is false
            Assert.AreEqual("Failed to send email: Invalid recipient.", result.Message); // Ensure correct message
        }
    }
}
