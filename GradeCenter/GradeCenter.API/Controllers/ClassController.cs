﻿using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GradeCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        // Admin only
        [HttpPost]
        public async Task<IActionResult> AddClass([FromBody] AddClassRequest request)
        {
            var result = await _classService.Add(request);
            if (result.Succeeded)
                return CreatedAtAction(nameof(AddClass), result.ReturnValue);
            else
                return BadRequest(result.Message);
        }

        // Admin only
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ClassDto classDto)
        {
            var result = await _classService.Edit(classDto);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetAll([FromQuery] Guid? schoolId = null)
        {
            var result = await _classService.GetAll(schoolId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _classService.GetById(id);

            return (result == null) ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _classService.Delete(id);

            return (result.Succeeded) ? Ok() : BadRequest(result.Message);
        }
    }
}
