using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class SubjectController : ApiBaseController
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetSubjects()
        {
            var data = await this._subjectRepository.GetSubjects();

            return Ok(data);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList([FromBody] SubjectRequest req)
        {
            var data = await this._subjectRepository.GetSubjectList(req);

            return Ok(data);
        }

        [HttpGet("GetHeadSubject/{userId}")]
        public async Task<IActionResult> GetHeadSubject(int userId)
        {
            var data = await this._subjectRepository.GetHeadSubjectList(userId);

            return Ok(data);
        }

        [HttpGet("GetLectureSubject/{userId}")]
        public async Task<IActionResult> GetLectureSubject(int userId)
        {
            var data = await this._subjectRepository.GetLectureSubjectList(userId);

            return Ok(data);
        }

        [HttpGet("GetSubjectById/{subjectId}")]
        public async Task<IActionResult> GetSubjectById(int subjectId)
        {
            var data = await this._subjectRepository.GetSubjectById(subjectId);

            return Ok(data);
        }

        [HttpPost("AddSubject")]
        public async Task<IActionResult> GetSubjects(SubjectRequest req)
        {
            var data = await this._subjectRepository.AddSubject(req);

            return Ok(data);
        }

        [HttpPut("UpdateSubject")]
        public async Task<IActionResult> UpdateSubject(SubjectRequest req)
        {
            var data = await this._subjectRepository.UpdateSubject(req);

            return Ok(data);
        }

        [HttpDelete("DeleteSubject/{subjectId}")]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            var data = await this._subjectRepository.DeleteSubject(subjectId);

            return Ok(data);
        }

        [HttpGet("GetSubjectByRole/{roleId}/{userId}/{campusId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubjectByRole(int roleId, int userId, int campusId)
        {
            var data = await this._subjectRepository.GetSubjectByRole(roleId, userId, campusId);

            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPost("ImportSubjectsFromExcel")]
        public async Task<IActionResult> ImportSubjectsFromExcel([FromForm] IFormFile file)
        {
            // Lấy thông tin người dùng hiện tại từ HttpContext
            var currentUser = HttpContext.User;
            var something = await this._subjectRepository.ImportSubjectsFromExcel(file, currentUser);
            return Ok(something);
        }

        [HttpPut("LecturerSubjectModify/{userId}")]
        public async Task<IActionResult> LecturerSubjectModify(int userId, HashSet<SubjectResponse> req)
        {
            var data = await this._subjectRepository.LecturerSubjectModify(userId, req);

            return Ok(data);
        }

        [HttpPost("AddSubjectToDepartment")]
        public async Task<IActionResult> AddSubjectToDepartment(SubjectDepartmentRequest req)
        {
            var data = await this._subjectRepository.AddSubjectToDepartment(req);

            return Ok(data);
        }
    }
}
