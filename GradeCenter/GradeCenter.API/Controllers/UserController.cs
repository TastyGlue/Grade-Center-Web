﻿namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest user)
        {
            var result = await _userService.AddUser(user);

            if (result.Succeeded)
                return CreatedAtAction(nameof(AddUser), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Authorize
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("\"Confirm Password\" does not match \"Password\"");

            var result = await _userService.ChangePassword(request);

            return result ? Ok() : BadRequest("Password change failed");
        }

        // Authorize
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UserDto userDto)
        {
            var authHeader = HttpContext.Request.Headers.Authorization;
            var result = await _userService.Edit(userDto, authHeader);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        // Admin only
        [HttpGet("no-role")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersWithoutRole()
        {
            var result = await _userService.GetUsersWithoutRoles();

            return Ok(result);
        }

        // Admin only
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var result = await _userService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }

        // Admin only
        [HttpPost("{userId}")]
        public async Task<IActionResult> RemoveFromRole(Guid userId)
        {
            var result = await _userService.RemoveFromRole(userId);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpPost("pending")]
        public async Task<IActionResult> AddPendingUser(AddUserRequest user)
        {
            var result = await _userService.AddPendingUser(user);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpPut("pending")]
        public async Task<IActionResult> EditPendingUser(PendingUserDto dto)
        {
            var result = await _userService.EditPendingUser(dto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpPost("pending/delete")]
        public async Task<IActionResult> DeletePendingUsers([FromBody] List<Guid> userIds)
        {
            var result = await _userService.DeletePendingUsers(userIds);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<PendingUserDto>>> GetAllPendingUsers()
        {
            var result = await _userService.GetAllPendingUsers();

            return Ok(result);
        }

        [HttpGet("pending/{id}")]
        public async Task<ActionResult<PendingUserDto?>> GetPendingUser(Guid id)
        {
            var result = await _userService.GetPendingUser(id);

            return Ok(result);
        }
    }
}
