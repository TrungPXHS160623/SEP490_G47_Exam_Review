﻿using AutoMapper;
using Library.Models;
using Library.Models.Dtos;
using Microsoft.AspNetCore.Http;
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
            try
            {
                var userDomainModels = await userRepository.GetAllAsync();
                var userDtos = mapper.Map<List<UserDto>>(userDomainModels);
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                // Log exception details for debugging purposes
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving users.");
            }
        }


        [HttpGet("get-all-with-filter")]
        public async Task<IActionResult> GetAllUserWithFilter([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            try
            {
                
                var userDomainModels = await userRepository.GetAllWithFilterAsync(filterOn, filterQuery);
                var userDtos = mapper.Map<List<UserDto>>(userDomainModels);
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                // Log exception details for debugging purposes
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving users.");
            }
        }
        [HttpGet("get-all-with-filter-and-sort")]
        public async Task<IActionResult> GetAllUserWithFilter([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? SortBy , [FromQuery] bool? IsAscending)
        {
            try
            {
                // Truyền filterOn và filterQuery vào GetAllWithFilterAsync
                var userDomainModels = await userRepository.GetAllWithFilterAsync(filterOn, filterQuery);
                var userDtos = mapper.Map<List<UserDto>>(userDomainModels);
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                // Log exception details for debugging purposes
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving users.");
            }
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var userDomainModel = await userRepository.GetByIdAsync(id);
                if (userDomainModel == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = mapper.Map<UserDto>(userDomainModel);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                // Log exception details for debugging purposes
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the user.");
            }
        }

        [HttpPost("create")]
        [ValidateModel]
        public async Task<IActionResult> CreateUser([FromBody] AddUserRequestDto addUserRequestDto)
        {
            try
            {
               
                var userDomainModel = mapper.Map<User>(addUserRequestDto);
                await userRepository.CreateAsync(userDomainModel);

                var userDto = mapper.Map<UserDto>(userDomainModel);
                return CreatedAtAction(nameof(GetUserById), new { id = userDto.UserId }, new { message = "User created successfully.", user = userDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
            }
        }

        [HttpPost("update/{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            try
            {
                var userDomainModel = mapper.Map<User>(updateUserRequestDto);
                var updatedUser = await userRepository.UpdateAsync(id, userDomainModel);

                if (updatedUser == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = mapper.Map<UserDto>(updatedUser);
                return Ok(new { message = "User updated successfully.", user = userDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
            }
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deletedUserDomainModel = await userRepository.DeleteAsync(id);

                if (deletedUserDomainModel == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = mapper.Map<UserDto>(deletedUserDomainModel);
                return Ok(new { message = "User deleted successfully.", user = userDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the user.");
            }
        }
    }
}
