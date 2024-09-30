using Library.Request;
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
    }
}
