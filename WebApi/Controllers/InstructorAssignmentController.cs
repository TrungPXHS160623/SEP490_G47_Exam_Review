using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class InstructorAssignmentController : ApiBaseController
    {
        private readonly ILogHistoryRepository _logHistoryRepository;
        private readonly IInstructorAssignmentRepository _instructorAssignmentRepository;

        public InstructorAssignmentController(IInstructorAssignmentRepository instructorAssignmentRepository, ILogHistoryRepository logHistoryRepository)
        {
            _instructorAssignmentRepository = instructorAssignmentRepository;
            _logHistoryRepository = logHistoryRepository;
        }

        [HttpPost("AssignExamToLecture")]
        public async Task<IActionResult> AssignExamToLecture([FromBody] LeaderExamResponse req)
        {
            var data = await this._instructorAssignmentRepository.AssignExamToLecture(req);
            await _logHistoryRepository.LogAsync($"Assign exam to lecturer {req.AssignedLectureName}", JwtToken);

            return Ok(data);
        }
        [HttpPost("AssignSubjectToLecture")]
        public async Task<IActionResult> AssignSubjectToLecture([FromBody] AddLecturerSubjectRequest req)
        {
            var data = await this._instructorAssignmentRepository.AssignSubjectToLecture(req);

            return Ok(data);
        }

        [HttpPost("SetAssignDate")]
        public async Task<IActionResult> SetAssignDate([FromBody] LectureExamResponse req)
        {
            var data = await this._instructorAssignmentRepository.SetAssignDate(req);
            await _logHistoryRepository.LogAsync($"Plan date to review", JwtToken);

            return Ok(data);
        }
    }
}
