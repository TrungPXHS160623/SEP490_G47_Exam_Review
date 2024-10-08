using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class RoleController : ApiBaseController
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet("GetRolesForAdmin")]
        public async Task<IActionResult> GetRolesForAdmin()
        {
            var data = await this._roleRepository.GetRolesForAdmin();

            return Ok(data);
        }

        [HttpGet("GetRolesForExaminer")]
        public async Task<IActionResult> GetRolesForExaminer()
        {
            var data = await this._roleRepository.GetRolesForExaminer();

            return Ok(data);
        }
    }
}
