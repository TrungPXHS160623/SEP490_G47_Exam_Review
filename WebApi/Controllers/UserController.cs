using AutoMapper;
using Library.Common;
using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class UserController : ApiBaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.config = config;
        }


        [HttpGet("GetLectureBySubject/{subjectId}/{campusId}")]
        public async Task<IActionResult> GetLecture(int subjectId, int campusId)
        {
            var data = await this.userRepository.GetLectureBySubject(subjectId, campusId);

            return Ok(data);
        }


        [HttpGet("GetUserForAdmin/{filterQuery?}")]
        public async Task<IActionResult> GetAllUserWithFilter(string filterQuery = null)
        {
            var userDomainModels = await userRepository.GetUserForAdmin(filterQuery);
            return Ok(userDomainModels);
        }

        [HttpGet("GetUserForExaminer/{userId}/{filterQuery?}")]
        public async Task<IActionResult> GetUserForExaminer(int userId, string filterQuery = null)
        {
            var userDomainModels = await userRepository.GetUserForExaminer(userId, filterQuery);
            return Ok(userDomainModels);
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var data = await this.userRepository.GetByIdAsync(id);

            return Ok(data);
        }

        [HttpGet("GetUserSubjectById/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserSubjectByIdAsync(int id)
        {
            var data = await this.userRepository.GetUserSubjectByIdAsync(id);

            return Ok(data);
        }

        [HttpGet("GetUserFacutyById/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserFacutyById(int id)
        {
            var data = await this.userRepository.GetUserFacutyByIdAsync(id);

            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
        {
            var data = await userRepository.CreateAsync(user);

            return Ok(data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserRequest updateUserRequestDto)
        {
            var updatedUser = await userRepository.UpdateAsync(updateUserRequestDto);
            return Ok(updatedUser);
        }

        [HttpPut("ExaminerUpdateUser")]
        public async Task<IActionResult> ExaminerUpdateUserAsync(UserSubjectRequest updateUserRequestDto)
        {
            var updatedUser = await userRepository.ExaminerUpdateUserAsync(updateUserRequestDto);
            return Ok(updatedUser);
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteUser = await userRepository.DeleteAsync(id);
            return Ok(deleteUser);
        }

        [HttpGet("GetHead/{subjectId}/{campusId}")]
        public async Task<IActionResult> GetHead(int subjectId, int campusId)
        {
            var deleteUser = await userRepository.GetHeadOfDepartment(subjectId, campusId);
            return Ok(deleteUser);
        }
        [AllowAnonymous]
        [HttpPost("ImportUsersFromExcel")]
        public async Task<IActionResult> ImportUsersFromExcel([FromForm] IFormFile file)
        {
            // Lấy thông tin người dùng hiện tại từ HttpContext
            var currentUser = HttpContext.User;
            var something = await userRepository.ImportUsersFromExcel(file, currentUser);
            return Ok(something);
        }

        [HttpGet("GetAssignedUser/{examId}")]
        public async Task<IActionResult> GetHead(int examId)
        {
            var data = await userRepository.GetAssignedUserByExam(examId);
            return Ok(data);
        }

        [HttpGet("GetLectureListByHead/{userId}")]
        public async Task<IActionResult> GetLectureListByHead(int userId)
        {
            var data = await userRepository.GetLectureListByHead(userId);
            return Ok(data);
        }

        [HttpGet("GoogleLoginCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginCallback(string code)
        {
            //if (!string.IsNullOrEmpty(error))
            //{
            //    if(error.Equals("access_denied"))
            //    return Redirect($"https://localhost:7158/login");
            //}

            var response = await userRepository.GoogleLoginCallback(code);
            if (response.IsSuccessful)
            {
                return Redirect($"https://localhost:7158/home");
            }

            var errorMsg = Uri.EscapeDataString(response.Message); // URL encode the error message
            return Redirect($"https://localhost:7158/login?errorMsg={errorMsg}");
        }

        [HttpGet("google-keys")]
        [AllowAnonymous]
        public IActionResult GetGoogleKeys()
        {
            var clientId = config["GoogleKeys:ClientId"];

            return Ok(new
            {
                ClientId = clientId,
            });
        }

        [HttpGet("GetJWT")]
        [AllowAnonymous]
        public IActionResult GetJWT()
        {
            return Ok(new AuthenticationResponse
            {
                IsSuccessful = true,
                Token = Constants.JWTToken,
            });
        }

        [HttpGet("ClearJWT")]
        [AllowAnonymous]
        public IActionResult ClearJWT()
        {
            Constants.JWTToken = string.Empty;

            return Ok(new RequestResponse
            {
                IsSuccessful = true,
            });
        }

        [HttpGet("GetUserBySubject/{subjectId}")]
        public async Task<IActionResult> GetUserBySubject(int subjectid)
        {
            var data = await this.userRepository.GetUserBySubject(subjectid);

            return Ok(data);
        }

        [HttpGet("GetUserByMail/{mail}/{headId}")]
        public async Task<IActionResult> GetUserByMail(string mail, int headId)
        {
            var data = await this.userRepository.GetUserByMail(mail, headId);

            return Ok(data);
        }

        [HttpPost("AddUserToSubject")]
        public async Task<IActionResult> AddUserToSubject([FromBody] AddLecturerSubjectRequest req)
        {
            var data = await this.userRepository.AddUserToSubject(req);

            return Ok(data);
        }
    }
}
