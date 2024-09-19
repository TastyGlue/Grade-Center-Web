namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] AddSubjectRequest request)
        {
            var result = await _subjectService.Add(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddSubject), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] SubjectDto subjectDto)
        {
            var result = await _subjectService.Edit(subjectDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll([FromQuery] string? schoolId = null)
        {
            var result = await _subjectService.GetAll(schoolId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _subjectService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
    }
}
