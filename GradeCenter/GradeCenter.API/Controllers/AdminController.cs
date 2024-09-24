namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] Guid userId)
        {
            var result = await _adminService.AddAdmin(userId);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddAdmin), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] AdminDto adminDto)
        {
            var result = await _adminService.Edit(adminDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        // Admin only
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAll()
        {
            var result = await _adminService.GetAll();

            return Ok(result);
        }

        // Admin only
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _adminService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
    }
}
