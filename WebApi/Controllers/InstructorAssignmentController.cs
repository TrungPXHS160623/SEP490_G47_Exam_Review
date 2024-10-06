using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class InstructorAssignmentController : ApiBaseController
    {
        private readonly IInstructorAssignmentRepository _instructorAssignmentRepository;

        public InstructorAssignmentController(IInstructorAssignmentRepository instructorAssignmentRepository)
        {
            _instructorAssignmentRepository = instructorAssignmentRepository;
        }

        [HttpPost("AssignExamToLecture")]
        public async Task<IActionResult> AssignExamToLecture([FromBody] LeaderExamResponse req)
        {
            var data = await this._instructorAssignmentRepository.AssignExamToLecture(req);

            return Ok(data);
        }

        [HttpPost("SetAssignDate")]
        public async Task<IActionResult> SetAssignDate([FromBody] LectureExamResponse req)
        {
            var data = await this._instructorAssignmentRepository.SetAssignDate(req);

            return Ok(data);
        }
    }
}
