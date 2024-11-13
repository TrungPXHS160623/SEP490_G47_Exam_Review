using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class FacultyController : ApiBaseController
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyController(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        [HttpGet("GetFaculties")]
        public async Task<IActionResult> GetFaculty()
        {
            var data = await this._facultyRepository.GetFaculties();

            return Ok(data);
        }

        [HttpGet("GetHeadFaculties/{userId}")]
        public async Task<IActionResult> GetHeadFaculty(int userId)
        {
            var data = await this._facultyRepository.GetHeadFaculties(userId);

            return Ok(data);
        }
    }
}
