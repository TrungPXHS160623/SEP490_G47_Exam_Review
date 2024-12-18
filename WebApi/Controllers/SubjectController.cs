﻿using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class SubjectController : ApiBaseController
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogHistoryRepository _logHistoryRepository;

        public SubjectController(ISubjectRepository subjectRepository, ILogHistoryRepository logHistoryRepository)
        {
            _subjectRepository = subjectRepository;
            _logHistoryRepository = logHistoryRepository;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubjects()
        {
            var data = await this._subjectRepository.GetSubjects();

            return Ok(data);
        }

        [HttpPost("GetList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList([FromBody] SubjectRequest req)
        {
            var data = await this._subjectRepository.GetSubjectList(req);

            return Ok(data);
        }

        [HttpGet("GetHeadSubject/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHeadSubject(int userId)
        {
            var data = await this._subjectRepository.GetHeadSubjectList(userId);

            return Ok(data);
        }

        [HttpGet("GetLectureSubject/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLectureSubject(int userId)
        {
            var data = await this._subjectRepository.GetLectureSubjectList(userId);

            return Ok(data);
        }

        [HttpGet("GetSubjectById/{subjectId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubjectById(int subjectId)
        {
            var data = await this._subjectRepository.GetSubjectById(subjectId);

            return Ok(data);
        }

        [HttpPost("AddSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubjects(SubjectRequest req)
        {
            var data = await this._subjectRepository.AddSubject(req);

            await _logHistoryRepository.LogAsync($"Add new subject [{req.SubjectCode}]", JwtToken);

            return Ok(data);
        }

        [HttpPut("UpdateSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateSubject(SubjectRequest req)
        {
            var data = await this._subjectRepository.UpdateSubject(req);
            await _logHistoryRepository.LogAsync($"update subject [{req.SubjectCode}]", JwtToken);

            return Ok(data);
        }

        [HttpDelete("DeleteSubject/{subjectId}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            var data = await this._subjectRepository.DeleteSubject(subjectId);
            await _logHistoryRepository.LogAsync($"Delete subject", JwtToken);

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

            await _logHistoryRepository.LogAsync($"Import subject from Excel", JwtToken);


            return Ok(something);
        }

        [HttpPut("LecturerSubjectModify/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> LecturerSubjectModify(int userId, HashSet<SubjectResponse> req)
        {
            var data = await this._subjectRepository.LecturerSubjectModify(userId, req);

            await _logHistoryRepository.LogAsync($"Update lecturer teaching subject", JwtToken);

            return Ok(data);
        }

        [HttpPost("AddSubjectToDepartment")]
        [AllowAnonymous]
        public async Task<IActionResult> AddSubjectToDepartment(SubjectDepartmentRequest req)
        {
            var data = await this._subjectRepository.AddSubjectToDepartment(req);

            await _logHistoryRepository.LogAsync($"Add subject to department", JwtToken);

            return Ok(data);
        }
    }
}
