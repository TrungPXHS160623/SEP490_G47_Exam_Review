using Moq;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class GenerateExcelRepositoryTests
    {
        private Mock<IGenerateExcelRepository> _mockGenerateExcelRepository;

        [SetUp]
        public void SetUp()
        {
            _mockGenerateExcelRepository = new Mock<IGenerateExcelRepository>();
        }

        [Test]
        public void GenerateExcel_ShouldReturnByteArray()
        {
            // Arrange
            var expectedBytes = new byte[] { 0x01, 0x02, 0x03 };
            _mockGenerateExcelRepository.Setup(repo => repo.GenerateExcel()).Returns(expectedBytes);

            // Act
            var result = _mockGenerateExcelRepository.Object.GenerateExcel();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedBytes, result);
        }

        [Test]
        public void GenerateExcelTime_ShouldReturnByteArray()
        {
            // Arrange
            var expectedBytes = new byte[] { 0x04, 0x05, 0x06 };
            _mockGenerateExcelRepository.Setup(repo => repo.GenerateExcelTime()).Returns(expectedBytes);

            // Act
            var result = _mockGenerateExcelRepository.Object.GenerateExcelTime();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedBytes, result);
        }

        [Test]
        public void GenerateExcelByStatus_ShouldReturnByteArray()
        {
            // Arrange
            int statusId = 1;
            var expectedBytes = new byte[] { 0x07, 0x08, 0x09 };
            _mockGenerateExcelRepository.Setup(repo => repo.GenerateExcelByStatus(statusId)).Returns(expectedBytes);

            // Act
            var result = _mockGenerateExcelRepository.Object.GenerateExcelByStatus(statusId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedBytes, result);
        }
    }
}
