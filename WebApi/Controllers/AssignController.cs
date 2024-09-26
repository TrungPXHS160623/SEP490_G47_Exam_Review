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
        [HttpGet("Get-All-Assign")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAssign()
        {
            var data = await assignRepository.GetAllAssign();
            return Ok(data);
        }

        [HttpGet("Get-All-Assign-With-CampusId/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAssignGetAllAssignByCampusId(int id)
        {
            var data = await assignRepository.GetAllAssignByCampusId(id);
            return Ok(data);
        }
        [HttpGet("Get-All-Assign-With-ExamId/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAssignGetAllAssignByExamId(int id)
        {
            var data = await assignRepository.GetAllAssignByExamId(id);
            return Ok(data);
        }
        [HttpGet("Get-All-Assign-With-HeadOfDepartment/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAssignGetAllAssignByHeadOfDepartmentId(int id)
        {
            var data = await assignRepository.GetAllAssignByHeadOfDepartmentId(id);
            return Ok(data);
        }
        [HttpGet("Get-All-Assign-With-LecturorId/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAssignGetAllAssignByLecturorId(int id)
        {
            var data = await assignRepository.GetAllAssignByLecturorId(id);
            return Ok(data);
        }

        [HttpPost("Create-Assign-InstructorAssign")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateInstructorAssign([FromBody] AssignRequest assignRequest)
        {
            var data = await assignRepository.AddAssign(assignRequest);
            return Ok(data);
        }




    }
}
