using AutoMapper;
using Library.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminerController : ControllerBase
    {
        private readonly IExaminerRepository examinerRepository;
        private readonly IMapper mapper;

        public ExaminerController(IExaminerRepository examinerRepository, IMapper mapper)
        {
            this.examinerRepository = examinerRepository;
            this.mapper = mapper;
        }

        [HttpGet("get-exams-by-campus/{examinerId:int}")]
        public async Task<IActionResult> GetExamsByCampus([FromRoute] int examinerId, [FromQuery] string? subjectName)
        {
            try
            {
                var exams = await examinerRepository.GetExamsByCampusAsync(examinerId, subjectName);

                if (exams == null || !exams.Any())
                {
                    return NotFound("No exams found for the given examiner.");
                }

                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving exams.");
            }
        }
        // API cập nhật trạng thái bài thi
        [HttpPut("{examId}/status")]
        public async Task<IActionResult> UpdateExamStatus(int examId, [FromBody] ExamStatusEnum status)
        {
            await examinerRepository.UpdateExamStatusAsync(examId, status);
            return Ok();
        }

    }
}
