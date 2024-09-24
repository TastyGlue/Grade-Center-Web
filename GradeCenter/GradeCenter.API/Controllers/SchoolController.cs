namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolRequest request)
        {
            var result = await _schoolService.Add(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddSchool), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] SchoolDto schoolDto)
        {
            var result = await _schoolService.Edit(schoolDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetAll()
        {
            var result = await _schoolService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _schoolService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
    }
}
