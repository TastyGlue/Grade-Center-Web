namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        // Admin and Teacher
        [HttpPost]
        public async Task<IActionResult> AddTimetable([FromBody] AddTimetableRequest request)
        {
            var result = await _timetableService.Add(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddTimetable), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] TimetableDto timetableDto)
        {
            var result = await _timetableService.Edit(timetableDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimetableDto>>> GetAll([FromQuery] Guid? classId = null, [FromQuery] int? teacherId = null)
        {
            var result = await _timetableService.GetAll(classId, teacherId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _timetableService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }

        // Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _timetableService.Delete(id);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }
    }
}
