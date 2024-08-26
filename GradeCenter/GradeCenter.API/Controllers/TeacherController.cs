namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] AddTeacherRequest request)
        {
            var result = await _teacherService.AddTeacher(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddTeacher), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int teacherId, [FromBody] TeacherDto teacherDto)
        {
            var result = await _teacherService.Edit(teacherId, teacherDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll([FromQuery] string? schoolId = null)
        {
            var result = await _teacherService.GetAll(schoolId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teacherService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
    }
}
