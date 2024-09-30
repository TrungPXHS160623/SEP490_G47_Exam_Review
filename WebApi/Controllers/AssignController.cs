using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignController : ApiBaseController
    {
        private IAssignRepository assignRepository;

        public AssignController(IAssignRepository assignRepository)
        {
            this.assignRepository = assignRepository;
        }
        [HttpGet("assignments/head-to-lecturers")]
        [AllowAnonymous]
        public async Task<IActionResult> ListAssignmentsToLecturersByHead()
        {
            var data = await assignRepository.ListAssignmentsToLecturersByHead();
            return Ok(data);
        }

        [HttpGet("assignments/by-campus/{campusId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAssignmentsByCampusId(int campusId)
        {
            var data = await assignRepository.GetAssignmentsByCampusId(campusId);
            return Ok(data);
        }
        [HttpGet("assignments/by-exam/{examId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAssignmentsByExamId(int examId)
        {
            var data = await assignRepository.GetAssignmentsByExamId(examId);
            return Ok(data);
        }

        [HttpGet("assignments/by-department-head/{headId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAssignmentsByHeadId(int headId)
        {
            var data = await assignRepository.GetAssignmentsByHeadId(headId);
            return Ok(data);
        }
        [HttpGet("assignments/by-lecturer/{lecturerId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAssignmentsByLecturerId(int lecturerId)
        {
            var data = await assignRepository.GetAssignmentsByLecturerId(lecturerId);
            return Ok(data);
        }

        [HttpPost("assignments/assign-to-lecturer")]
        [AllowAnonymous]
        public async Task<IActionResult> AddAssignToLecturer([FromBody] AssignRequest assignRequest)
        {
            var data = await assignRepository.AddAssignToLecturer(assignRequest);
            return Ok(data);
        }
    }
}
