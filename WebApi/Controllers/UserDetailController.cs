using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ApiBaseController
    {
        private readonly IUserDetailRepository _userDetailRepository;

        public UserDetailController(IUserDetailRepository userDetailRepository)
        {
            this._userDetailRepository = userDetailRepository;
        }
        
        [HttpGet("GetUserDetails")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserDetails()
        {
            var response = await _userDetailRepository.GetUserDetailsAsync();
            if (response.IsSuccessful)
            {
                return Ok(response.Items);
            }
            return BadRequest(response.Message);
        }

       
        [HttpGet("GetUserDetailByUserId/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserDetailByUserId(int userId)
        {
            var response = await _userDetailRepository.GetUserDetailByUserIdAsync(userId);
            if (response.IsSuccessful)
            {
                return Ok(response.Item);
            }

            return NotFound(response.Message);
        }

        
        [HttpPost("CreateUserDetail")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserDetail([FromBody]UserDetailRequest request)
        {
            var response = await _userDetailRepository.CreateUserDetailAsync(request);
            if (response.IsSuccessful)
            {
                return CreatedAtAction(nameof(GetUserDetailByUserId), new { userId = response.Content }, response.Message);
            }

            return BadRequest(response.Message);
        }

       
        [HttpPut("UpdateUserDetail/{userDetailId}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUserDetail(int userDetailId, UserDetailRequest request)
        {
            var response = await _userDetailRepository.UpdateUserDetailAsync(userDetailId, request);
            if (response.IsSuccessful)
            {
                return Ok(response.Message);
            }

            return BadRequest(response.Message);
        }

        
        [HttpDelete("DeleteUserDetail/{userDetailId}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUserDetail(int userDetailId)
        {
            var success = await _userDetailRepository.DeleteUserDetailAsync(userDetailId);
            if (success)
            {
                return Ok("Delete succesfully!!!"); 
            }

            return NotFound("User detail not found.");
        }

        
        [HttpPut("ToggleStatus/{userDetailId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ToggleUserDetailStatus(int userDetailId)
        {
            var response = await _userDetailRepository.ToggleUserDetailStatusAsync(userDetailId);
            if (response.IsSuccessful)
            {
                return Ok(response.Message);
            }

            return BadRequest(response.Message);
        }
    }
}
