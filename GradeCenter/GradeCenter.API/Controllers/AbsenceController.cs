namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        // Admin and Teacher
        [HttpPost]
        public async Task<IActionResult> AddAbsence([FromBody] AddAbsenceRequest request)
        {
            var result = await _absenceService.Add(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddAbsence), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin and Teacher
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] AbsenceDto absenceDto)
        {
            var result = await _absenceService.Edit(absenceDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbsenceDto>>> GetAll([FromQuery] int? studentId = null)
        {
            var result = await _absenceService.GetAll(studentId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _absenceService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }

        // Admin and Teacher
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _absenceService.Delete(id);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }
    }
}
