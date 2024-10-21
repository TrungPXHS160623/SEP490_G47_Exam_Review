using Library.Models;
using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;
using WebApi.Repository;

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

        [HttpGet("GetSubjectById/{subjectId}")]
        public async Task<IActionResult> GetSubjectById(int subjectId)
        {
            var data = await this._subjectRepository.GetSubjectById(subjectId);

            return Ok(data);
        }

        [HttpPost("AddSubject")]
        public async Task<IActionResult> GetSubjects(Subject req)
        {
            var data = await this._subjectRepository.AddSubject(req);

            return Ok(data);
        }

        [HttpPut("UpdateSubject")]
        public async Task<IActionResult>UpdateSubject(Subject req)
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
    }
}
