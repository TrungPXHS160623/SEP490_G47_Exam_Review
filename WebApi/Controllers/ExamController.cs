
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class ExamController : ApiBaseController
    {
        private readonly IExamRepository _examRepository;

        public ExamController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetExamInfo()
        {
            var examInfo = await _examRepository.GetExamInfoAsync();
            return Ok(examInfo);
        }

        [HttpPost("GetExamList")]
        public async Task<IActionResult> GetExamList([FromBody] ExamSearchRequest req)
        {
            var examInfo = await _examRepository.GetExamList(req);
            return Ok(examInfo);
        }

        [HttpPost("GetLeaderExamList")]
        public async Task<IActionResult> GetLeaderExamList([FromBody] ExamSearchRequest req)
        {
            var examInfo = await _examRepository.GetLeaderExamList(req);
            return Ok(examInfo);
        }

        [HttpPost("GetLectureExamList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLectureExamList([FromBody] ExamSearchRequest req)
        {
            var examInfo = await _examRepository.GetLectureExamList(req);
            return Ok(examInfo);
        }


        [HttpGet("GetExamById/{examId}")]
        public async Task<IActionResult> GetExamById(int examId)
        {
            var examInfo = await _examRepository.GetExamById(examId);
            return Ok(examInfo);
        }

        [HttpGet("GetLeaderExamById/{examId}")]
        public async Task<IActionResult> GetLeaderExamById(int examId)
        {
            var examInfo = await _examRepository.GetLeaderExamById(examId);
            return Ok(examInfo);
        }

        [HttpGet("GetLectureExamById/{examId}")]
        public async Task<IActionResult> GetLectureExamById(int examId)
        {
            var examInfo = await _examRepository.GetLectureExamById(examId);
            return Ok(examInfo);
        }

        [HttpPut("UpdateExam")]
        public async Task<IActionResult> UpdateExam([FromBody] TestDepartmentExamResponse req)
        {
            var examInfo = await _examRepository.UpdateExam(req);
            return Ok(examInfo);
        }

        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatusExam([FromBody] List<TestDepartmentExamResponse> req)
        {
            var examInfo = await _examRepository.ChangeStatusExam(req);
            return Ok(examInfo);
        }

        [HttpPut("ChangeStatus/{examid}")]
        public async Task<IActionResult> ChangeStatusExam(int examId, [FromBody] int statusId)
        {
            var examInfo = await _examRepository.ChangeStatusExamById(examId, statusId);
            return Ok(examInfo);
        }

        [HttpPost("CreateExam")]
        public async Task<IActionResult> CreateExam(ExamCreateRequest req)
        {
            var examInfo = await _examRepository.CreateExam(req);
            return Ok(examInfo);
        }
        [AllowAnonymous]
        [HttpPost("ImportExamsFromExcel")]
        public async Task<IActionResult> ImportExamsFromExcel([FromForm] IFormFile file)
        {
            var something = await _examRepository.ImportExamsFromExcel(file);
            return Ok(something);
        }

    }
}
