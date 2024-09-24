using AutoMapper;
using Library.Models;
using Library.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApi.CustomActionFilter;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await this.userRepository.GetAllAsync();

            return Ok(data);
        }


        [HttpGet("get-all-with-filter")]
        public async Task<IActionResult> GetAllUserWithFilter([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var userDomainModels = await userRepository.GetAllWithFilterAsync(filterOn, filterQuery);
            var userDtos = mapper.Map<List<UserDto>>(userDomainModels);
            return Ok(userDtos);
        }
        [HttpGet("get-all-with-filter-and-sort")]
        public async Task<IActionResult> GetAllUserWithFilter([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? SortBy, [FromQuery] bool? IsAscending)
        {
            // Truyền filterOn và filterQuery vào GetAllWithFilterAsync
            var userDomainModels = await userRepository.GetAllWithFilterAsync(filterOn, filterQuery);
            var userDtos = mapper.Map<List<UserDto>>(userDomainModels);
            return Ok(userDtos);
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var data = await this.userRepository.GetByIdAsync(id);

            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var data = await userRepository.CreateAsync(user);

            return Ok(data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(User updateUserRequestDto)
        {
            var updatedUser = await userRepository.UpdateAsync(updateUserRequestDto);
            return Ok(updatedUser);
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteUser = await userRepository.DeleteAsync(id);
            return Ok(deleteUser);
        }
    }
}
