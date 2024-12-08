using Library.Common;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.IRepository;


namespace MyApi.Tests
{
    [TestFixture]
    public class CampusControllerTests
    {
        private Mock<ICampusRepository> _mockCampusRepository;
        private CampusController _campusController;

        [SetUp]
        public void SetUp()
        {
            _mockCampusRepository = new Mock<ICampusRepository>();
            _campusController = new CampusController(_mockCampusRepository.Object);
        }

        [Test]
        public async Task GetCampus_ReturnsOkResult_WithListOfCampuses()
        {
            var campuses = new List<Campus>
            {
                new Campus { CampusId = 1, CampusName = "Hoa Lac" },
                new Campus { CampusId = 2, CampusName = "Da Nang" }
            };

            _mockCampusRepository
           .Setup(repo => repo.GetCampus())
           .ReturnsAsync(new ResultResponse<Campus>
           {
               Items = campuses,
               IsSuccessful = true,
               Message = "Retrieved successfully"
           });

            var result = await _campusController.GetCampus();
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            var response = okResult.Value as ResultResponse<Campus>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(2, response.Items.Count);

        }

        [Test]
        public async Task AddCampus_ReturnsOkResult_WithAddedCampus()
        {
            // Arrange
            var newCampus = new Campus { CampusId = 3, CampusName = "New Campus" };

            _mockCampusRepository
                .Setup(repo => repo.AddCampus(It.IsAny<Campus>()))
                .ReturnsAsync(new ResultResponse<Campus>
                {
                    Items = new List<Campus> { newCampus },
                    IsSuccessful = true,
                    Message = "Added successfully"
                });

            // Act
            var result = await _campusController.AddCampus(newCampus);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as ResultResponse<Campus>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("New Campus", response.Items.First().CampusName);
        }
        [Test]
        public async Task UpdateCampus_ReturnsOkResult_WithUpdatedCampus()
        {
            // Arrange
            var updatedCampus = new Campus { CampusId = 1, CampusName = "Updated Campus" };

            _mockCampusRepository
                .Setup(repo => repo.UpdateCampus(It.IsAny<Campus>()))
                .ReturnsAsync(new ResultResponse<Campus>
                {
                    Items = new List<Campus> { updatedCampus },
                    IsSuccessful = true,
                    Message = "Updated successfully"
                });

            // Act
            var result = await _campusController.UpdateCampus(updatedCampus);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as ResultResponse<Campus>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("Updated Campus", response.Items.First().CampusName);
        }
        [Test]
        public async Task DeleteCampus_ReturnsOkResult_WhenCampusDeleted()
        {
            // Arrange
            int campusIdToDelete = 1;

            _mockCampusRepository
                .Setup(repo => repo.DeleteCampus(It.IsAny<int>()))
                .ReturnsAsync(new ResultResponse<bool>
                {
                    Items = new List<bool> { true },
                    IsSuccessful = true,
                    Message = "Deleted successfully"
                });

            // Act
            var result = await _campusController.DelteCampus(campusIdToDelete);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as ResultResponse<bool>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(response.Items.First());
        }
        [Test]
        public async Task GetCampusById_ReturnsOkResult_WithCampus()
        {
            // Arrange
            int campusId = 1;
            var campus = new Campus { CampusId = 1, CampusName = "Hoa Lac" };

            _mockCampusRepository
                .Setup(repo => repo.GetCampusById(It.IsAny<int>()))
                .ReturnsAsync(new ResultResponse<Campus>
                {
                    Items = new List<Campus> { campus },
                    IsSuccessful = true,
                    Message = "Retrieved successfully"
                });

            // Act
            var result = await _campusController.GetCampusById(campusId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as ResultResponse<Campus>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("Hoa Lac", response.Items.First().CampusName);
        }
        [Test]
        public async Task GetCampusById_ReturnsNotFound_WhenCampusNotExists()
        {
            // Arrange
            int campusId = 1;

            _mockCampusRepository
                .Setup(repo => repo.GetCampusById(It.IsAny<int>()))
                .ReturnsAsync(new ResultResponse<Campus>
                {
                    Items = new List<Campus>(),
                    IsSuccessful = false,
                    Message = "Campus not found"
                });

            // Act
            var result = await _campusController.GetCampusById(campusId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

    }
}
