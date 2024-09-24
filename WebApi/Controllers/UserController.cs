using AutoMapper;
using Library.Models;
using Library.Models.Dtos;
using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.CustomActionFilter;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class UserController : ApiBaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await this.userRepository.GetAllAsync();

            return Ok(data);
        }


        [HttpGet("get-all-with-filter/{filterQuery}")]
        public async Task<IActionResult> GetAllUserWithFilter(string filterQuery)
        {
            var userDomainModels = await userRepository.GetAllWithFilterAsync(filterQuery);
            return Ok(userDomainModels);
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
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
        {
            var data = await userRepository.CreateAsync(user);

            return Ok(data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserRequest updateUserRequestDto)
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
