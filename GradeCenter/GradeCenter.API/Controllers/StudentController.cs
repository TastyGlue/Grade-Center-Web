namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
            var result = await _studentService.AddStudent(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddStudent), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int studentId, [FromBody] StudentDto studentDto)
        {
            var result = await _studentService.Edit(studentId, studentDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll([FromQuery] string? schoolId = null)
        {
            var result = await _studentService.GetAll(schoolId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
    }
}
