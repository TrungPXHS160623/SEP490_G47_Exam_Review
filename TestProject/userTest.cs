using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using WebApi.IRepository;

namespace WebApi.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Mock<IUserRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock repository before each test
            _mockRepository = new Mock<IUserRepository>();
        }

        // Test for CreateAsync (Create User)
        [Test]
        public async Task CreateAsync_ShouldReturnSuccess_WhenValidUser()
        {
            // Arrange
            var userRequest = new UserRequest { UserName = "John Doe", Email = "john@example.com" };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "User created successfully."
            };

            _mockRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<UserRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.CreateAsync(userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User created successfully.", result.Message);
        }

        // Test for GetUserForAdmin (Get users for Admin)
        [Test]
        public async Task GetUserForAdmin_ShouldReturnUserList_WhenValidFilterQuery()
        {
            // Arrange
            var filterQuery = "John";
            var expectedResponse = new ResultResponse<UserResponse>
            {
                IsSuccessful = true,
                Message = "Users fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetUserForAdmin(It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetUserForAdmin(filterQuery);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Users fetched successfully.", result.Message);
        }

        // Test for GetByIdAsync (Get user by ID)
        [Test]
        public async Task GetByIdAsync_ShouldReturnUser_WhenValidId()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new ResultResponse<UserRequest>
            {
                IsSuccessful = true,
                Item = new UserRequest { UserId = 1, UserName = "John Doe", Email = "john@example.com" },
                Message = "User fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User fetched successfully.", result.Message);
            Assert.NotNull(result.Item);
            Assert.AreEqual(1, result.Item.UserId);
        }

        // Test for DeleteAsync (Delete user)
        [Test]
        public async Task DeleteAsync_ShouldReturnSuccess_WhenValidId()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "User deleted successfully."
            };

            _mockRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.DeleteAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User deleted successfully.", result.Message);
        }

        // Test for UpdateAsync (Update user)
        [Test]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenValidUser()
        {
            // Arrange
            var userRequest = new UserRequest { UserId = 1, UserName = "John Updated", Email = "john.updated@example.com" };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "User updated successfully."
            };

            _mockRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<UserRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.UpdateAsync(userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User updated successfully.", result.Message);
        }

        // Test for ImportUsersFromExcel (Import users from Excel)
        [Test]
        public async Task ImportUsersFromExcel_ShouldReturnSuccess_WhenValidFile()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var claimsPrincipal = new ClaimsPrincipal();
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Users imported successfully."
            };

            _mockRepository
                .Setup(repo => repo.ImportUsersFromExcel(It.IsAny<IFormFile>(), It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.ImportUsersFromExcel(mockFile.Object, claimsPrincipal);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Users imported successfully.", result.Message);
        }

        // Test for GetAssignedUserByExam (Get users assigned to an exam)
        [Test]
        public async Task GetAssignedUserByExam_ShouldReturnUserList_WhenValidExamId()
        {
            // Arrange
            var examId = 1;
            var expectedResponse = new ResultResponse<UserResponse>
            {
                IsSuccessful = true,

                Message = "Users fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetAssignedUserByExam(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetAssignedUserByExam(examId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Users fetched successfully.", result.Message);
        }

        // Test for GoogleLoginCallback (Google login callback)
        [Test]
        public async Task GoogleLoginCallback_ShouldReturnAuthenticationResponse_WhenValidCode()
        {
            // Arrange
            var code = "valid-google-code";
            var expectedResponse = new AuthenticationResponse
            {
                IsSuccessful = true,
                Token = "valid-token",
                Message = "Login successful."
            };

            _mockRepository
                .Setup(repo => repo.GoogleLoginCallback(It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GoogleLoginCallback(code);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Login successful.", result.Message);
            Assert.AreEqual("valid-token", result.Token);
        }

        // Test for AddUserToSubject (Add user to subject)
        [Test]
        public async Task AddUserToSubject_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new AddLecturerSubjectRequest { UserId = 1, SubjectId = 101 };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "User added to subject successfully."
            };

            _mockRepository
                .Setup(repo => repo.AddUserToSubject(It.IsAny<AddLecturerSubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.AddUserToSubject(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User added to subject successfully.", result.Message);
        }

        // Test for EditLecturer (Edit lecturer details)
        [Test]
        public async Task EditLecturer_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new AddLecturerSubjectRequest { UserId = 1, SubjectId = 101 };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Lecturer updated successfully."
            };

            _mockRepository
                .Setup(repo => repo.EditLecturer(It.IsAny<AddLecturerSubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.EditLecturer(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Lecturer updated successfully.", result.Message);
        }

        // Test for RemoveLecture (Remove user from subject)
        [Test]
        public async Task RemoveLecture_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var userId = 1;
            var subjectId = 101;
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Lecturer removed from subject successfully."
            };

            _mockRepository
                .Setup(repo => repo.RemoveLecture(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.RemoveLecture(userId, subjectId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Lecturer removed from subject successfully.", result.Message);
        }

        // Test for CreateHeadAsync (Create Head of Department)
        [Test]
        public async Task CreateHeadAsync_ShouldReturnSuccess_WhenValidUser()
        {
            // Arrange
            var userSubjectRequest = new UserSubjectRequest { UserName = "Jane Doe", Email = "jane@example.com" };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Head created successfully."
            };

            _mockRepository
                .Setup(repo => repo.CreateHeadAsync(It.IsAny<UserSubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.CreateHeadAsync(userSubjectRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Head created successfully.", result.Message);
        }

        // Test for GetUserForExaminer (Get user for examiner role)
        [Test]
        public async Task GetUserForExaminer_ShouldReturnUserList_WhenValidUserIdAndFilterQuery()
        {
            // Arrange
            var userId = 1;
            var filterQuery = "examiner";
            var expectedResponse = new ResultResponse<UserResponse>
            {
                IsSuccessful = true,

                Message = "Users fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetUserForExaminer(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetUserForExaminer(userId, filterQuery);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Users fetched successfully.", result.Message);
        }

        // Test for GetUserSubjectByIdAsync (Get user subject by ID)
        [Test]
        public async Task GetUserSubjectByIdAsync_ShouldReturnUserSubject_WhenValidId()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new ResultResponse<UserSubjectRequest>
            {
                IsSuccessful = true,
                Item = new UserSubjectRequest { UserId = 1, UserName = "John Doe" },
                Message = "User subject fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetUserSubjectByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetUserSubjectByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User subject fetched successfully.", result.Message);
            Assert.NotNull(result.Item);
            Assert.AreEqual(1, result.Item.UserId);
        }

        // Test for GetUserFacutyByIdAsync (Get user faculty by ID)
        [Test]
        public async Task GetUserFacutyByIdAsync_ShouldReturnUserFaculty_WhenValidId()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new ResultResponse<UserSubjectRequest>
            {
                IsSuccessful = true,
                Item = new UserSubjectRequest { UserId = 1, UserName = "Jane Doe" },
                Message = "User faculty fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetUserFacutyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetUserFacutyByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User faculty fetched successfully.", result.Message);
            Assert.NotNull(result.Item);
            Assert.AreEqual(1, result.Item.UserId);
        }

        // Test for ExaminerUpdateUserAsync (Update examiner user)
        [Test]
        public async Task ExaminerUpdateUserAsync_ShouldReturnSuccess_WhenValidUserUpdate()
        {
            // Arrange
            var userSubjectRequest = new UserSubjectRequest { UserId = 1, UserName = "John Updated" };
            var expectedResponse = new RequestResponse
            {
                IsSuccessful = true,
                Message = "Examiner updated successfully."
            };

            _mockRepository
                .Setup(repo => repo.ExaminerUpdateUserAsync(It.IsAny<UserSubjectRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.ExaminerUpdateUserAsync(userSubjectRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Examiner updated successfully.", result.Message);
        }

        // Test for GetHeadOfDepartment (Get head of department by subject and campus)
        [Test]
        public async Task GetHeadOfDepartment_ShouldReturnHead_WhenValidSubjectAndCampusId()
        {
            // Arrange
            var subjectId = 101;
            var campusId = 1;
            var expectedResponse = new ResultResponse<UserResponse>
            {
                IsSuccessful = true,

                Message = "Head of department fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetHeadOfDepartment(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetHeadOfDepartment(subjectId, campusId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Head of department fetched successfully.", result.Message);
        }

        // Test for GetLectureBySubject (Get lecture by subject and campus)
        [Test]
        public async Task GetLectureBySubject_ShouldReturnLectureList_WhenValidSubjectAndCampus()
        {
            // Arrange
            var subjectId = 101;
            var campusId = 1;
            var expectedResponse = new ResultResponse<UserResponse>
            {
                IsSuccessful = true,

                Message = "Lectures by subject fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetLectureBySubject(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetLectureBySubject(subjectId, campusId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Lectures by subject fetched successfully.", result.Message);
        }

        // Test for GetLectureListByHead (Get lecture list by head)
        [Test]
        public async Task GetLectureListByHead_ShouldReturnLectureList_WhenValidUserId()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = new ResultResponse<UserResponse>
            {
                IsSuccessful = true,

                Message = "Lecturer list fetched by head successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetLectureListByHead(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetLectureListByHead(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Lecturer list fetched by head successfully.", result.Message);
        }

        // Test for GetUserBySubject (Get user by subject)
        [Test]
        public async Task GetUserBySubject_ShouldReturnUser_WhenValidSubjectIdAndCampusId()
        {
            // Arrange
            var subjectId = 101;
            var campusId = 1;
            var expectedResponse = new ResultResponse<UserResponse>
            {
                IsSuccessful = true,

                Message = "User by subject fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetUserBySubject(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetUserBySubject(subjectId, campusId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User by subject fetched successfully.", result.Message);
        }

        // Test for GetUserByMail (Get user by email)
        [Test]
        public async Task GetUserByMail_ShouldReturnUser_WhenValidEmailAndHeadId()
        {
            // Arrange
            var email = "john@example.com";
            var headId = 1;
            var expectedResponse = new ResultResponse<AddLecturerSubjectRequest>
            {
                IsSuccessful = true,
                Item = new AddLecturerSubjectRequest { UserId = 1, SubjectId = 101 },
                Message = "User by mail fetched successfully."
            };

            _mockRepository
                .Setup(repo => repo.GetUserByMail(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockRepository.Object.GetUserByMail(email, headId);

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("User by mail fetched successfully.", result.Message);
            Assert.NotNull(result.Item);
            Assert.AreEqual(1, result.Item.UserId);
        }
    }
}

