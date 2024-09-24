namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddParent([FromBody] AddParentRequest request)
        {
            var result = await _parentService.AddParent(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddParent), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ParentDto teacherDto)
        {
            var result = await _parentService.Edit(teacherDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentDto>>> GetAll()
        {
            var result = await _parentService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _parentService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
    }
}
