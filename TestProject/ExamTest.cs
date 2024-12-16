namespace WebApi.Tests
{
    [TestFixture]
    public class ExamRepositoryTests
    {
        private Mock<IExamRepository> _mockExamRepository;

        [SetUp]
        public void SetUp()
        {
            _mockExamRepository = new Mock<IExamRepository>();
        }

        [Test]
        public async Task GetExamInfoAsync_ShouldReturnListOfExamInfoDto()
        {
            // Arrange
            var examInfoList = new List<ExamInfoDto>
            {
                new ExamInfoDto {  ExamCode = "Math Exam" },
                new ExamInfoDto {  ExamCode = "Science Exam" }
            };

            _mockExamRepository.Setup(repo => repo.GetExamInfoAsync())
                .ReturnsAsync(examInfoList);

            // Act
            var result = await _mockExamRepository.Object.GetExamInfoAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetExamById_ShouldReturnCorrectExamResponse()
        {
            // Arrange
            int examId = 1;
            var expectedResponse = new ResultResponse<ExaminerExamResponse>
            {
                IsSuccessful = true,
                Items = new List<ExaminerExamResponse>
                {
                    new ExaminerExamResponse { ExamId = examId, ExamCode = "Math Exam" }
                }
            };

            _mockExamRepository.Setup(repo => repo.GetExamById(examId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.GetExamById(examId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(examId, result.Items.First().ExamId);
        }

        [Test]
        public async Task CreateExam_ShouldReturnSuccessResponse()
        {
            // Arrange
            var examRequest = new ExamCreateRequest
            {
                ExamCode = "New Exam",
                SemesterId = 1,
                SubjectId = 2
            };

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Exam created successfully."
            };

            _mockExamRepository.Setup(repo => repo.CreateExam(examRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.CreateExam(examRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Exam created successfully.", result.Message);
        }

        [Test]
        public async Task ImportExamsFromExcel_ShouldReturnSuccessResponse()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var mockUser = new ClaimsPrincipal();

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Exams imported successfully."
            };

            _mockExamRepository.Setup(repo => repo.ImportExamsFromExcel(mockFile.Object, mockUser))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.ImportExamsFromExcel(mockFile.Object, mockUser);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Exams imported successfully.", result.Message);
        }
        [Test]
        public async Task ChangeStatusExamById_ShouldReturnSuccessResponse()
        {
            // Arrange
            int examId = 1;
            int statusId = 2;
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Exam status updated successfully."
            };

            _mockExamRepository.Setup(repo => repo.ChangeStatusExamById(examId, statusId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.ChangeStatusExamById(examId, statusId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Exam status updated successfully.", result.Message);
        }

        [Test]
        public async Task GetLeaderExamList_ShouldReturnLeaderExamResponses()
        {
            // Arrange
            var request = new ExamSearchRequest
            {
                CampusId = 1,
                SemesterId = 2
            };

            var expectedResponse = new ResultResponse<LeaderExamResponse>
            {
                IsSuccessful = true,
                Items = new List<LeaderExamResponse>
                {
                    new LeaderExamResponse { ExamId = 1, ExamCode = "Leader Exam 1" },
                    new LeaderExamResponse { ExamId = 2, ExamCode = "Leader Exam 2" }
                }
            };

            _mockExamRepository.Setup(repo => repo.GetLeaderExamList(request))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.GetLeaderExamList(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(2, result.Items.Count);
        }
        [Test]
        public async Task UpdateExam_ShouldReturnSuccessResponse()
        {
            // Arrange
            var examUpdate = new ExaminerExamResponse { ExamId = 1, ExamCode = "Updated Exam" };

            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Exam updated successfully."
            };

            _mockExamRepository.Setup(repo => repo.UpdateExam(examUpdate))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.UpdateExam(examUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Exam updated successfully.", result.Message);
        }

        [Test]
        public async Task GetExamByCampusAndSubject_ShouldReturnCampusSubjectExamResponse()
        {
            // Arrange
            var userRequest = new UserRequest { CampusId = 1 };

            var expectedResponse = new ResultResponse<CampusSubjectExamResponse>
            {
                IsSuccessful = true,
                Items = new List<CampusSubjectExamResponse>()
            };

            _mockExamRepository.Setup(repo => repo.GetExamByCampusAndSubject(userRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.GetExamByCampusAndSubject(userRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(1, result.Items.Count);
        }
        [Test]
        public async Task GetCampusReport_ShouldReturnCampusSubjectExamResponse()
        {
            // Arrange

            var expectedResponse = new ResultResponse<CampusReportResponse>
            {
                IsSuccessful = true,
                Items = new List<CampusReportResponse>()
            };

            _mockExamRepository.Setup(repo => repo.GetCampusReport())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.GetCampusReport();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(1, result.Items.Count);
        }
        [Test]
        public async Task GetDepartmentReport()
        {
            // Arrange
            var userRequest = new UserRequest { CampusId = 1 };

            var expectedResponse = new ResultResponse<DepartmentReportResponse>
            {
                IsSuccessful = true,
                Items = new List<DepartmentReportResponse>()
            };

            _mockExamRepository.Setup(repo => repo.GetDepartmentReport(userRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockExamRepository.Object.GetDepartmentReport(userRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(1, result.Items.Count);
        }
    }
}
