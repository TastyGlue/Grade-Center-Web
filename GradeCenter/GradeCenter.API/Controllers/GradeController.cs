namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        // Admin and Teacher
        [HttpPost]
        public async Task<IActionResult> AddGrade([FromBody] AddGradeRequest request)
        {
            var result = await _gradeService.Add(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddGrade), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin and Teacher
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] GradeDto gradeDto)
        {
            var result = await _gradeService.Edit(gradeDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetAll([FromQuery] string? classId = null, [FromQuery] int? teacherId = null)
        {
            var result = await _gradeService.GetAll(classId, teacherId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _gradeService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }

        // Admin and Teacher
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _gradeService.Delete(id);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }
    }
}
