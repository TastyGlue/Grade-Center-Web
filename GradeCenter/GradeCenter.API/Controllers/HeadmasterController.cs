﻿namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadmasterController : ControllerBase
    {
        private readonly IHeadmasterService _headmasterService;

        public HeadmasterController(IHeadmasterService headmasterService)
        {
            _headmasterService = headmasterService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddHeadmaster([FromBody] AddHeadmasterRequest request)
        {
            var result = await _headmasterService.AddHeadmaster(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddHeadmaster), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] HeadmasterDto headmasterDto)
        {
            var result = await _headmasterService.Edit(headmasterDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeadmasterDto>>> GetAll([FromQuery] Guid? schoolId = null)
        {
            var result = await _headmasterService.GetAll(schoolId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _headmasterService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
    }
}
